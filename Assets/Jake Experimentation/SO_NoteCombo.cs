using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_NoteCombo", menuName = "Scriptable Objects/SO_NoteCombo")]
public class SO_NoteCombo : ScriptableObject
{
    [Header("Sound Effect Result")]
    public EventInstance soundEffect;
    [Header("Combo of Notes")]
    public string comboNumbers;
}
