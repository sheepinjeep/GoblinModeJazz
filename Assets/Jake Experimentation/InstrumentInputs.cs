using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Saxaphone))]
public class InstrumentInputs : MonoBehaviour
{
    // --- INPUTS ---
    // Input Context
    private PlayerInputs playerInputContext;
    // Notes
    private List<InputAction> noteInputs = new List<InputAction>();
    
    // --- NOTE PARTICLES ---
    public ParticleSystem MusicNotes;
    private bool noteSystemExists = false;

    // --- SAX ---
    [Tooltip("Saxophone :)")]
    private Saxaphone saxophone;
    
    
    
    private void Start()
    {
        // Grab Ref to Sax
        saxophone = GetComponent<Saxaphone>();
        if (saxophone == null)
            Debug.LogWarning("InstrumentInputs: Hey Boss I need a Saxophone to play.");
        
        // --- INPUTS ---
        // Get Player Inputs
        playerInputContext =  new PlayerInputs();
        playerInputContext.InstrumentNotes.Enable();
        
        // Collect Note Inputs
        noteInputs.Add(playerInputContext.InstrumentNotes.A1);
        noteInputs.Add(playerInputContext.InstrumentNotes.B);
        noteInputs.Add(playerInputContext.InstrumentNotes.C);
        noteInputs.Add(playerInputContext.InstrumentNotes.D);
        noteInputs.Add(playerInputContext.InstrumentNotes.E);
        noteInputs.Add(playerInputContext.InstrumentNotes.F);
        noteInputs.Add(playerInputContext.InstrumentNotes.G);
        noteInputs.Add(playerInputContext.InstrumentNotes.A2);
        
        // Assign Events to Note Inputs
        for (int i = 0; i < noteInputs.Count; i++)
        {
            int note = i;
            noteInputs[i].performed += ctx => saxophone.StartNote(note);
            noteInputs[i].canceled += ctx => saxophone.EndNote();
        }
        
        // Octave Shifting Inputs
        playerInputContext.InstrumentNotes.OctaveUp.performed += ctx => saxophone.ShiftOctave(1);
        playerInputContext.InstrumentNotes.OctaveDown.performed += ctx => saxophone.ShiftOctave(-1);
        
        
        // --- PARTICLES --- 
        noteSystemExists = (MusicNotes != null);
        Debug.Log(noteSystemExists);
    }
    
    private void PlayMusicNotes()
    {
        if (noteSystemExists)
        {
            if (saxophone.IsPlaying())
            {
                MusicNotes.Play();
            }
            else
            {
                MusicNotes.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}
