using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;


public class KeyChanger : MonoBehaviour
{
    [SerializeField] private Saxaphone saxaphone;
    [SerializeField] private MusicScale[] keys;
    [SerializeField] private StudioEventEmitter eventEmitter;
    private EVENT_CALLBACK markerCallback;
    private Dictionary<string, MusicScale> KeyDict = new Dictionary<string,MusicScale>();

    void Start()
    {   
        foreach(MusicScale key in keys)
        {
            KeyDict.Add(key.name,key);
        }

        eventEmitter.Play();
        //args have to be name of desired function the trigger
        markerCallback = new EVENT_CALLBACK(ChangeKey);
        //do this because the event might get garbage collected if you dont?
        eventEmitter.EventInstance.setCallback(markerCallback, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }

    private FMOD.RESULT ChangeKey(EVENT_CALLBACK_TYPE type, IntPtr eventPtr, IntPtr parameters)
    {
        if (type == EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
        {
            // Retrieve the marker properties
            var marker = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(TIMELINE_MARKER_PROPERTIES));

            if (!KeyDict.ContainsKey(marker.name))
            {
                Debug.Log("Warning: " + marker.name + "does not correspond to a key in the database");
                return FMOD.RESULT.OK;
            }
            
            if (saxaphone == null)
                return FMOD.RESULT.OK;
            
            saxaphone.currentScale = KeyDict[marker.name];
        }
        return FMOD.RESULT.OK;
    }

}
