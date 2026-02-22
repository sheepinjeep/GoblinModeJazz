using System;
using UnityEngine;

public class HappyGoblins : MonoBehaviour
{
    public bool test;
    public Animator[] animators;

    void Update()
    {
        if (test)
        {
            test = !test;
            BeHappy();
        }
    }

    public void BeHappy()
    {
        foreach(Animator a in animators)
        {
            a.SetTrigger("Happy");
        }
    } 
}
