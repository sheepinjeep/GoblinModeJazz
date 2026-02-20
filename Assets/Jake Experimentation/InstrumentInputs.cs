using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentInputs : MonoBehaviour
{
    private PlayerInputs playerInputContext;
    
    private InputAction Ainput;
    private InputAction Binput;
    private InputAction Cinput;
    private InputAction Dinput;
    private InputAction Einput;
    private InputAction Finput;
    private InputAction Ginput;
    private InputAction A2input;

    private void Start()
    {
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
        if (Ainput.IsPressed())
            Debug.Log("Do");

        if (Binput.IsPressed())
            Debug.Log("Re");
        
        if (Cinput.IsPressed())
            Debug.Log("Mi");
        
        if (Dinput.IsPressed())
            Debug.Log("Fa");
        
        if (Einput.IsPressed())
            Debug.Log("So");
        
        if (Finput.IsPressed())
            Debug.Log("La");
        
        if (Ginput.IsPressed())
            Debug.Log("Ti");
        
        if (A2input.IsPressed())
            Debug.Log("Do");
    }
}
