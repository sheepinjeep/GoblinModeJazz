using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Linq;

public class Saxaphone : MonoBehaviour
{
    public int note = 0;
    public InputActionReference testKeyEvent1;
    public InputActionReference testKeyEvent2;

    public MusicScale currentScale;

    private EventInstance saxaphoneEvent;
    [Range(-24f, 24f)]
    public float pitchSmoothTime = 0.1f;
    public float pitchValue = 0f;
    private float desiredPitchValue = 0f;
    private float pitchVelocity = 0f;

    private bool notePlaying = false;

    private bool note1Playing = false;
    private bool note2Playing = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        //placeholder stuff for real implementation
        testKeyEvent1.action.performed += ctx => {StartNote(); SetNote(0); note1Playing = true;};
        testKeyEvent2.action.performed += ctx => {StartNote(); SetNote(note); note2Playing = true;};

        testKeyEvent1.action.canceled += ctx => { note1Playing = false; EndNote();};
        testKeyEvent2.action.canceled += ctx => { note2Playing = false; EndNote();};

        testKeyEvent1.action.Enable();
        testKeyEvent2.action.Enable();

        saxaphoneEvent = RuntimeManager.CreateInstance("event:/SaxNote");
    }

    private void Update()
    {
        // Smooths going from the current pitch to the new pitch
        pitchValue = Mathf.SmoothDamp(pitchValue, desiredPitchValue, ref pitchVelocity, pitchSmoothTime);
        saxaphoneEvent.setParameterByName("Pitch", pitchValue);
    }

    public void StartNote()
    {
        if (notePlaying) return;
        notePlaying = true;
        saxaphoneEvent.setParameterByName("End", 0f);
        saxaphoneEvent.start();
    }

    public void EndNote()
    {
        if (note1Playing || note2Playing) return;
        saxaphoneEvent.setParameterByName("End", 1f);
        notePlaying = false;
    }

    //translates from integer of a note in a scale to semitones
    public void SetNote(int note)
    {
        float semiToneValue = 0;

        int scaleLength = currentScale.intervals.Length;

        if (note < 0)
        {
            //subtract semitones from the end to the front of of the scale
            for (int i = 0; i < -note; i++)
            {
                semiToneValue -= currentScale.intervals[(scaleLength-1)-(i%scaleLength)];
            } 
        }
        else
        {
            //adds semitones from the start to the end of the array
            for (int i = 0; i <= note; i++)
            {
                semiToneValue += currentScale.intervals[i%scaleLength];
            } 
        }   
        if (!notePlaying) pitchValue = semiToneValue;     
        desiredPitchValue = semiToneValue;
    }

}
