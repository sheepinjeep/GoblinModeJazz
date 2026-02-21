using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentInputs : MonoBehaviour
{
    // --- INPUTS ---
    // Input Context
    private PlayerInputs playerInputContext;
    // Notes
    private InputAction Ainput;
    private InputAction Binput;
    private InputAction Cinput;
    private InputAction Dinput;
    private InputAction Einput;
    private InputAction Finput;
    private InputAction Ginput;
    private InputAction A2input;
    
    // --- NOTE PARTICLES ---
    public ParticleSystem MusicNotes;
    private bool noteSystemExists = false;
    private bool notesPlaying = false;
    
    private void Start()
    {
        noteSystemExists = (MusicNotes != null);
        Debug.Log(noteSystemExists);
        
        playerInputContext =  new PlayerInputs();
        playerInputContext.InstrumentNotes.Enable();
        
        Ainput = playerInputContext.InstrumentNotes.A1;
        Binput = playerInputContext.InstrumentNotes.B;
        Cinput = playerInputContext.InstrumentNotes.C;
        Dinput = playerInputContext.InstrumentNotes.D;
        Einput = playerInputContext.InstrumentNotes.E;
        Finput = playerInputContext.InstrumentNotes.F;
        Ginput = playerInputContext.InstrumentNotes.G;
        A2input = playerInputContext.InstrumentNotes.A2;
    }

    private void Update()
    {
        ParseInputs();
    }
    
    
    private void ParseInputs()
    {
        notesPlaying = false;
        
        if (Ainput.IsPressed())
        {
            //Debug.Log("Do");
            notesPlaying = true;
        }
            

        if (Binput.IsPressed())
        {
            //Debug.Log("Re");
            notesPlaying = true;
        }
            
        
        if (Cinput.IsPressed())
        {
            //Debug.Log("Mi");
            notesPlaying = true;
        }
        
        if (Dinput.IsPressed())
        {
            //Debug.Log("Fa");
            notesPlaying = true;
        }
        
        if (Einput.IsPressed())
        {
            //Debug.Log("So");
            notesPlaying = true;
        }
        
        if (Finput.IsPressed())
        {
            //Debug.Log("La");
            notesPlaying = true;
        }
        
        if (Ginput.IsPressed())
        {
            //Debug.Log("Ti");
            notesPlaying = true;
        }
        
        if (A2input.IsPressed())
        {
            //Debug.Log("Do");
            notesPlaying = true;
        }
        
        
        PlayMusicNotes();
    }

    private void PlayMusicNotes()
    {
        if (noteSystemExists)
        {
            if (notesPlaying)
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
