using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using System.Runtime.InteropServices;
using System;


public class KeyChanger : MonoBehaviour
{
    public StudioEventEmitter eventEmitter;
    private EVENT_CALLBACK markerCallback;

    void Start()
    {   eventEmitter.Play();
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
            //you can get the name which is really cool
            //will probably put key change in dict and use string keys?
            Debug.Log("Marker Reached: " + marker.name);

            // *** Place your Unity code logic here ***
            // You can trigger animations, UI changes, etc. based on marker.name
        }
        return FMOD.RESULT.OK;
    }

}
