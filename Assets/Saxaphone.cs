using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.InputSystem;


public class Saxaphone : MonoBehaviour
{
    public InputActionReference testKeyEvent1;
    public InputActionReference testKeyEvent2;

    public MusicScale currentScale;

    private EventInstance saxaphoneEvent;
    [Range(-24f, 24f)]
    public float pitchSmoothTime = 0.1f;
    public float pitchValue = 0f;
    private float desiredPitchValue = 0f;
    private float pitchVelocity = 0f;

    private int noteCounter = 0;

    private void Awake() {
        //placeholder stuff for real implementation
        testKeyEvent1.action.performed += ctx => StartNote(randomNote());
        testKeyEvent2.action.performed += ctx => StartNote(randomNote());

        testKeyEvent1.action.canceled += ctx => EndNote();
        testKeyEvent2.action.canceled += ctx => EndNote();

        testKeyEvent1.action.Enable();
        testKeyEvent2.action.Enable();

        saxaphoneEvent = RuntimeManager.CreateInstance("event:/SaxNote");
    }

    private int randomNote(int range = 8)
    {
        int result = Random.Range(-range,range);
        return result;
    }

    private void Update()
    {
        // Smooths going from the current pitch to the new pitch
        pitchValue = Mathf.SmoothDamp(pitchValue, desiredPitchValue, ref pitchVelocity, pitchSmoothTime);
        saxaphoneEvent.setParameterByName("Pitch", pitchValue);
    }

    //change pitch to new note, and start playing if not already playing
    public void StartNote(int note)
    {
        noteCounter++;

        SetNote(note);

        if (noteCounter > 1) return;

        saxaphoneEvent.setParameterByName("End", 0f);
        saxaphoneEvent.start();
    }

    public void EndNote()
    {
        noteCounter--;

        if(noteCounter > 0) return;

        saxaphoneEvent.setParameterByName("End", 1f);
    }

    //translates from integer of a note in a scale to semitones
    private void SetNote(int note)
    {
        float semiToneValue = 0;

        int scaleLength = currentScale.intervals.Length;

        if (note < 0)
        {
            //subtract semitones from the end to the front of of the scale
            for (int i = 0; i < -note; i++)
            {
                semiToneValue -= currentScale.intervals[scaleLength-1-(i%scaleLength)];
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
        if (noteCounter <= 1) pitchValue = semiToneValue;     
        desiredPitchValue = semiToneValue;
    }

}
