using UnityEngine;

[CreateAssetMenu(fileName = "New Scale", menuName = "Music Scale")]
public class MusicScale : ScriptableObject
{
    [SerializeField]private float startNote = 0;
    [SerializeField]private ScaleType scaleType;

    private int[][] intervals =
    {
        new int[] {0,2,2,1,2,2,2,1},
        new int[] {0,2,1,2,2,1,2,2},
        new int[] {0,2,2,1,2,2,1,2},
        new int[] {0,2,1,2,2,1,3,1},
        new int[] {0,2,2,2,1,2,2,1}
    };

    public int[] GetScale()
    {
        return intervals[(int)scaleType];
    }

    public float getStartNote()
    {
        return startNote;
    }
}

public enum ScaleType
{
    Major,
    Minor,
    Mixolydian,
    HarmonicMinor,
    Lydian
}
