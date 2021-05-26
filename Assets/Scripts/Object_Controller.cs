using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Controller : MonoBehaviour
{

    public Animator animator;

    public AudioSource HittedEffect;

    private GameObject collectibleInside;


    void Start()
    {

        if (this.gameObject.transform.childCount != 0)
        {

            collectibleInside = this.gameObject.transform.GetChild(0).gameObject;

        }

    }


    public void getHitted()
    {

        if (StateGame.audioState == 1)
        {

            HittedEffect.Play();

        }

        animator.SetTrigger("Broken");

        GetComponent<Collider2D>().enabled = false;

        if (collectibleInside != null)
        {

            collectibleInside.SetActive(true);

        }

    }

}
