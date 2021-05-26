using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnOff : MonoBehaviour
{

    public Toggle audioToggle;


    private void Start()
    {

        if (StateGame.audioState == 1)
        {

            audioToggle.isOn = true;

        }

        else
        {

            audioToggle.isOn = false;

        }


        audioToggle.onValueChanged.AddListener(
            delegate
            {
                ToggleValueChanged(audioToggle);
            });

    }


    //Output the new state of the Toggle into Text when the user uses the Toggle
    private void ToggleValueChanged(Toggle change)
    {

        if (audioToggle.isOn)
        {

            StateGame.audioState = 1;

        }

        else
        {

            StateGame.audioState = 0;

        }

    }

}
