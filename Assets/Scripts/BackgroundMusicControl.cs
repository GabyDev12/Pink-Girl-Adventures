using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicControl : MonoBehaviour
{

    private AudioSource music;


    void Start()
    {

        music = gameObject.GetComponent<AudioSource>();
        
        music.loop = true;

    }


    void Update()
    {

        if (StateGame.audioState == 1)
        {

            if (!music.isPlaying)
            {
            
                music.Play();
            
            }

        }

        else
        {

            if (music.isPlaying)
            {
                
                music.Stop();

            }

        }

    }

}
