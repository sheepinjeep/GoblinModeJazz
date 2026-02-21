using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;

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
    private List<Color>  colours = new List<Color>();
    
    
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
        
        // Add colours for notes
        colours.Add( new Color(230/255f, 38/255f, 31/255f));
        colours.Add( new Color(235/255f, 117/255f, 50/255f));
        colours.Add( new Color(247/255f, 208/255f, 56/255f));
        colours.Add( new Color(163/255f, 224/255f, 72/255f));
        colours.Add( new Color(73/255f, 218/255f, 154/255f));
        colours.Add( new Color(52/255f, 187/255f, 230/255f));
        colours.Add( new Color(67/255f, 85/255f, 219/255f));
        colours.Add( new Color(210/255f, 59/255f, 231/255f));
    }

    private void Update()
    {
        PlayMusicNotes();
    }
    
    private void PlayMusicNotes()
    {
        if (noteSystemExists)
        {
            if (saxophone.IsPlaying())
            {
                if (!MusicNotes.isEmitting)
                {
                    var main = MusicNotes.main;
                    main.startColor = colours[saxophone.GetCurrentNote()];
                    Debug.Log(colours[saxophone.GetCurrentNote()]);
                    MusicNotes.Play();
                }

            }
            else
            {
                MusicNotes.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
    
    
}
