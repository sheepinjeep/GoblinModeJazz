using UnityEngine;
using UnityEngine.UI;

public class FadeInImageOnStart : MonoBehaviour
{
    public Image image;
    public float speed = 3;
    public float lerpTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime += Mathf.Min(1, lerpTime + Time.deltaTime/speed);

        image.color = new Color( image.color.r, image.color.g, image.color.b, 1-lerpTime);

    }
}
