using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_NoteCombo", menuName = "Scriptable Objects/SO_NoteCombo")]
public class SO_NoteCombo : ScriptableObject
{
    public List<int> comboNumbers = new List<int>();
}
