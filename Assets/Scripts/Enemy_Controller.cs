using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{

    public float speed;

    public bool moveRight;

    public Animator animator;

    public AudioSource HittedEffect;


    private Character_Controller sciptCharacter;


    void Start()
    {

        sciptCharacter = GameObject.Find("Character").GetComponent<Character_Controller>();

    }


    void Update()
    {
        if (!GameControl.instance.gameOver)
        {

            if (moveRight && !GameControl.instance.pauseActive)
            {

                transform.Translate(2 * Time.deltaTime * speed, 0, 0);

                transform.localScale = new Vector2(2, 2);

            }

            else if (!GameControl.instance.pauseActive)
            {

                transform.Translate(-2 * Time.deltaTime * speed, 0, 0);

                transform.localScale = new Vector2(-2, 2);

            }

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("LimitEnemy"))
        {

            if (moveRight)
            {

                moveRight = false;
            
            }

            else
            {

                moveRight = true;
            
            }

        }


        // When collide with the character, it will be damage
        if (collision.gameObject.CompareTag("Player"))
        {

            sciptCharacter.getDamage();

        }

    }


    public void getHitted()
    {

        animator.SetBool("TakeHit", true);

        if (StateGame.audioState == 1)
        {

            HittedEffect.Play();

        }

        GetComponent<Collider2D>().enabled = false;

    }

}
