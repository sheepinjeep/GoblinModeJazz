using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_NoteCombo", menuName = "Scriptable Objects/SO_NoteCombo")]
public class SO_NoteCombo : ScriptableObject
{
    [Header("Sound Effect")]
    public EventInstance soundEffect;
    public string  soundEffectName;
    [Header("Combo of Notes")]
    public string comboNumbers;
    
    public void CreateEventInstance()
    {
        soundEffect = RuntimeManager.CreateInstance(soundEffectName);
    }
}
