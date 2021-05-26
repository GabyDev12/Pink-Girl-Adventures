using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public Animator sceneTransition;

    public Animator animator;

    public Text interactText;


    public AudioSource chestEffect;
    public AudioSource interactEffect;


    private bool isInRange = false;

    private bool openChest = false;

    private bool interacting = false;

    private GameObject collectibleInside;

    private Character_Controller sciptCharacter;


    void Start()
    {

        sciptCharacter = GameObject.Find("Character").GetComponent<Character_Controller>();

        if (this.gameObject.transform.childCount != 0)
        {

            collectibleInside = this.gameObject.transform.GetChild(0).gameObject;

        }

    }


    void Update()
    {

        if (isInRange && !GameControl.instance.pauseActive)
        {

            // First door of the game
            if (CompareTag("Initial_Door"))
            {

                interactText.text = "Press 'E' to start the adventure";

                if (Input.GetButtonDown("Interact"))
                {

                    if (StateGame.audioState == 1)
                    {

                        interactEffect.Play();

                    }

                    StartCoroutine(LoadLevel("GameScene"));

                }

            }

            // Door to enter the boss fight 
            if (CompareTag("Boss_Door"))
            {

                if (sciptCharacter.keyCollected)
                {

                    interactText.text = "Press 'E' to start the boss fight";

                    if (Input.GetButtonDown("Interact"))
                    {

                        if (StateGame.audioState == 1)
                        {

                            interactEffect.Play();

                        }

                        StartCoroutine(LoadLevel("BossScene"));

                    }

                }

                else
                {

                    interactText.text = "You need a key to open the door";

                }

            }

            // Chest
            if (CompareTag("Chest"))
            {

                if (!openChest)
                {

                    interactText.text = "Press 'E' to open";

                    if (Input.GetButtonDown("Interact"))
                    {

                        openChest = true;

                        animator.SetTrigger("Open");

                        if (StateGame.audioState == 1)
                        {

                            chestEffect.Play();

                        }

                        GetComponent<Collider2D>().enabled = false;

                        if (collectibleInside != null)
                        {

                            collectibleInside.SetActive(true);

                        }

                    }

                }

            }


            // Tombstone
            if (CompareTag("Tombstone"))
            {

                if (!interacting)
                {

                    interactText.text = "Press 'E'";

                    if (Input.GetButtonDown("Interact"))
                    {

                        interacting = true;

                        if (StateGame.audioState == 1)
                        {

                            interactEffect.Play();

                        }

                        interactText.text = "";

                        StartCoroutine(temporalText("Here is the body of the last person who fight to the boss. Be careful!"));

                    }

                }
                
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            isInRange = true;

            interactText.gameObject.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            isInRange = false;

            interactText.gameObject.SetActive(false);

        }

    }


    // Coroutine for load the level with transition
    IEnumerator LoadLevel(string sceneName)
    {

        // Play animation
        sceneTransition.SetTrigger("Start");

        // Wait seconds
        yield return new WaitForSeconds(1);

        // Load scene
        SceneManager.LoadScene(sceneName);

    }


    // Coroutine for show a temporal info text
    IEnumerator temporalText(string text)
    {

        // Show text
        sciptCharacter.infoText.text = text;

        sciptCharacter.infoText.gameObject.SetActive(true);

        // Wait seconds
        yield return new WaitForSeconds(3);

        // Hide text
        sciptCharacter.infoText.gameObject.SetActive(false);

        interacting = false;

    }

}
