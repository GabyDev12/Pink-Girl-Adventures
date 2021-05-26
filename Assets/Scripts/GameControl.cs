using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    public static GameControl instance; // A reference to our game control script so we can access it statically.
    
    public GameObject gameOvertext; // A reference to the object that displays the text which appears when the player dies.

    public bool gameOver = false; // Is the game over?

    public bool gameEnd = false;


    public GameObject gameOverMenu;
    public GameObject gameEndMenu;


    public GameObject pauseMenu;
    public bool pauseActive = false;


    private bool audioON;

    public AudioSource gameAudio;
    public AudioSource gameOverAudio;
    public AudioSource gameEndAudio;


    private Character_Controller sciptCharacter;


    void Awake()
    {

        sciptCharacter = GameObject.Find("Character").GetComponent<Character_Controller>();

        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;

            //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

    }


    void Update()
    {

        // Check audio preference
        if (StateGame.audioState == 1)
        {

            audioON = true;


            // Play game music
            if (!gameAudio.isPlaying && audioON)
            {

                gameAudio.Play();

            }

        }

        else
        {

            audioON = false;

            gameAudio.Stop();

        }


        // If Esc is pressed
        if (Input.GetButtonDown("Pause"))
        {

            ShowPauseMenu();

        }

    }


    public void ShowPauseMenu()
    {

        pauseMenu.SetActive(true);

        pauseActive = true;

    }

    public void HidePauseMenu()
    {

        pauseMenu.SetActive(false);

        pauseActive = false;

    }


    public void CharacterDead()
    {

        // Activate the game over menu
        StartCoroutine(loadGameMenu("GameOver"));


        // Set the game to be over
        gameOver = true;


        // Disable the movement of the rigidbody
        sciptCharacter.GetComponent<Rigidbody2D>().simulated = false;


        // Stop the music and play the game over music
        if (audioON)
        {

            gameAudio.gameObject.SetActive(false);

            gameOverAudio.Play();

        }

    }


    public void BossDead()
    {

        // Activate the game end menu
        StartCoroutine(loadGameMenu("GameEnd"));


        // Set the game to be end
        gameEnd = true;


        // Stop the music and play the gmae end music
        if (audioON)
        {

            gameAudio.gameObject.SetActive(false);

            gameEndAudio.Play();

        }

    }


    // Coroutine for show a game over menu after a 2 seconds
    IEnumerator loadGameMenu(string menu)
    {

        // Wait seconds
        yield return new WaitForSeconds(2);


        if (menu.Equals("GameOver"))
        {

            // Show game over menu
            gameOverMenu.SetActive(true);

        }

        else if (menu.Equals("GameEnd"))
        {
            
            // Show game end menu
            gameEndMenu.SetActive(true);

        }

    }

}
