using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{

    void Start()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");


        if (objs.Length >= 1)
        {

            foreach (GameObject o in objs)
            {

                Destroy(o);

            }

        }

    }

}
