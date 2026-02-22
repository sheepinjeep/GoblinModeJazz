using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class MainMenu : MonoBehaviour
{
    [Header("Object Refs")]
    public Image AlbumCover;
    public Image VinylRecord;
    public Image FadePannel;
    public Canvas canvas;
    public StudioEventEmitter ambience;


    [Header("Record Pop Out")]
    public Vector2 popOutPos;
    public AnimationCurve popOutCurve;
    public float popOutSpeed = 1;
    private float popOutTime = 0;
    private bool popState = false;
    
    [Header("Game Start Sequence")]
    public AnimationCurve leaveCurve;
    public float leaveSpeed = 0.5f;
    public float fadeOutSpeed = 3f;
    public float recordRotationSpeed = 1;
    public string sceneName;
    private float leaveTime = 0;
    private bool leaveState = false;
    private Vector2 offScreen;
    private float fadeOutTime;
    private bool sceneRequested = false;

    private EventInstance slideOutShort, slideInShort, slideOutLong;

    void Start()
    {
        StartCoroutine(LoadScene());
        slideOutShort = RuntimeManager.CreateInstance("event:/SlideOutShort");
        slideInShort = RuntimeManager.CreateInstance("event:/SlideInShort");
        slideOutLong = RuntimeManager.CreateInstance("event:/SlideOutLong");
    }

    // Update is called once per frame
    void Update()
    {
        if (leaveState)
        {
            leaveTime = Mathf.Min(leaveTime + Time.deltaTime / leaveSpeed, 1);

            offScreen.x = -(canvas.GetComponent<RectTransform>().sizeDelta.x + VinylRecord.rectTransform.sizeDelta.x)/2;

            AlbumCover.rectTransform.anchoredPosition =  Vector2.LerpUnclamped(Vector2.zero, offScreen, leaveCurve.Evaluate(leaveTime));
            VinylRecord.rectTransform.anchoredPosition =  Vector2.LerpUnclamped(popOutPos, Vector2.zero, leaveCurve.Evaluate(leaveTime));
            VinylRecord.rectTransform.Rotate(0,0,Time.deltaTime * recordRotationSpeed * leaveCurve.Evaluate(leaveTime));

            if (leaveTime >= 1)
            {
                ambience.SetParameter("ToVinyl",1);
                fadeOutTime = Mathf.Min(fadeOutTime + Time.deltaTime / fadeOutSpeed, 1);
                FadePannel.color = new Vector4(FadePannel.color.r,FadePannel.color.g,FadePannel.color.b, fadeOutTime);

                if(fadeOutTime >= 1)
                {
                    sceneRequested = true;
                }
            }
            return;
        }

        if (popState)
        {
            popOutTime = Mathf.Min(popOutTime + Time.deltaTime / popOutSpeed, 1);
        }
        else
        {
            popOutTime = Mathf.Max(popOutTime - Time.deltaTime / popOutSpeed, 0);
        }
        VinylRecord.rectTransform.anchoredPosition = Vector2.LerpUnclamped(Vector2.zero, popOutPos, popOutCurve.Evaluate(popOutTime));
    }

    public void PopOut()
    {
        popState = true;
        slideOutShort.start();
    }

    public void PopIn()
    {
        popState = false;
        slideInShort.start();
    }

    public void startGame()
    {
        leaveState = true;
        slideOutLong.start();
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Wait to you press the space key to activate the Scene
                if (sceneRequested)
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}
