using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public Animator sceneTransition;

    public AudioSource buttonPressedEffect;


    public void LoadScene(string sceneName)
    {

        if (StateGame.audioState == 1)
        {

            buttonPressedEffect.Play();

        }

        SceneManager.LoadScene(sceneName);

    }


    public void ExitGame()
    {

        if (StateGame.audioState == 1)
        {

            buttonPressedEffect.Play();

        }

        Application.Quit();

    }

}
