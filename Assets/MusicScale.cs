using UnityEngine;

[CreateAssetMenu(fileName = "New Scale", menuName = "Music Scale")]
public class MusicScale : ScriptableObject
{
    public int[] intervals = {0,2,2,1,2,2,2,1}; // Major scale intervals by default
}
