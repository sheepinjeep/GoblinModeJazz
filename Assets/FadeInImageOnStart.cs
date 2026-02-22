using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImageOnStart : MonoBehaviour
{
    public CinemachineCamera startCam;
    public CinemachineCamera[] cameras;
    private CinemachineCamera usingCamera;
    public CinemachineSplineDolly dolly;
    public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0,0,1,1);
    public Image image;
    public float speed = 3;
    public float lerpTime = 0;
    private bool startingState = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime = Mathf.Min(2, lerpTime + Time.deltaTime/speed);

        if (startingState)
        {
            image.color = new Color( image.color.r, image.color.g, image.color.b, 1-animationCurve.Evaluate(lerpTime));
            dolly.CameraPosition = animationCurve.Evaluate(lerpTime/2);

            if(lerpTime >= 2) startingState = false;

            return;
        }

        if(lerpTime >= 2)
        {
            
        }


    }
}
