using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "Music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class DontDestroy : MonoBehaviour
{

    void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");


        if (objs.Length > 1)
        {

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);

    }

}