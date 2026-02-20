using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Saxaphone : MonoBehaviour
{

    public InputActionReference testKeyEvent1;
    public InputActionReference testKeyEvent2;

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
        testKeyEvent1.action.performed += ctx => {StartNote(); SetNote(0); note1Playing = true;};
        testKeyEvent2.action.performed += ctx => {StartNote(); SetNote(2); note2Playing = true;};

        testKeyEvent1.action.canceled += ctx => { note1Playing = false; EndNote();};
        testKeyEvent2.action.canceled += ctx => { note2Playing = false; EndNote();};

        testKeyEvent1.action.Enable();
        testKeyEvent2.action.Enable();

        saxaphoneEvent = RuntimeManager.CreateInstance("event:/SaxNote");
        //StartNote();
    }

    private void Update()
    {
        // Smoothly interpolate the pitch value towards the desired pitch value
        pitchValue = Mathf.SmoothDamp(pitchValue, desiredPitchValue, ref pitchVelocity, pitchSmoothTime);
        saxaphoneEvent.setParameterByName("Pitch", pitchValue);
    }

    public void StartNote()
    {
        if (notePlaying) return;
        notePlaying = true;
        saxaphoneEvent.setParameterByName("End", 1f);
        saxaphoneEvent.start();
    }

    public void EndNote()
    {
        if (note1Playing || note2Playing) return;
        saxaphoneEvent.setParameterByName("End", 0f);
        notePlaying = false;
    }

    public void SetNote(int note)
    {
        desiredPitchValue = note;
    }

}
