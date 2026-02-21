using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;


public class Saxaphone : MonoBehaviour
{
    [Header("Which scale to use")]
    public MusicScale currentScale;

    private EventInstance saxaphoneEvent;
    
    [Header("Pitch Settings")]
    [Range(-24f, 24f)]
    public float pitchSmoothTime = 0.1f;
    public float pitchValue = 0f;
    private float desiredPitchValue = 0f;
    private float pitchVelocity = 0f;

    private int noteCounter = 0;

    private int currentOctave = 0;
    
    private void Awake() {
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
    private void SetNote(int localNote)
    {
        float semiToneValue = 0;

        int scaleLength = currentScale.intervals.Length;
        
        int note = (currentOctave * 7) +  localNote;
        
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
    /// <summary>
    /// Shifts the octave up or down based on the given amount.
    /// </summary>
    public void ShiftOctave(int shiftAmount)
    {
        currentOctave = Math.Clamp(currentOctave + shiftAmount, -2, 1);
        Debug.Log(currentOctave);
    }
    
    /// <summary>
    /// Returns whether the saxophone is currently playing notes.
    /// </summary>
    public bool IsPlaying()
    {
        if (noteCounter > 1)
            return true;
        
        return false;
    }
}
