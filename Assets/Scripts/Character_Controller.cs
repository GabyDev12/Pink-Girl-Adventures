using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character_Controller : MonoBehaviour
{

	public Animator animator;

	private Rigidbody2D rb;

	public Text infoText;


	public AudioSource JumpEffect1;
	public AudioSource JumpEffect2;
	public AudioSource RunningEffect;
	public AudioSource HittedEffect;

	public AudioSource CoinEffect;
	public AudioSource CollectEffect;


	public float runSpeed;
	public float jumpForce;
	private float movementSmooth = .05f;
	private Vector3 velocity = Vector3.zero;

	private float horizontalMove;

	private bool facingRight = true;

	private bool isGrounded;
	public Transform groundCheck;
	public LayerMask groundLayer;

	private bool doubleJump;


	// Coins
	public Text coinsAmount_Text;
	private int coinsCollected = 0;


	// Key
	public bool keyCollected = false;


	// Health
	private int hearts = 2;
	private int boostHearts;
	private GameObject heartsLeft;
	private GameObject heartsLost;
	private GameObject heartsBoost;



	private void Start()
	{

		animator = GetComponent<Animator>();

		rb = GetComponent<Rigidbody2D>();

		heartsLeft = GameObject.Find("Left");
		heartsLost = GameObject.Find("Lost");


        if (StateGame.runeCollected)
        {

			heartsBoost = GameObject.Find("Boost");

			// Enable the boost hearts
			heartsBoost.transform.GetChild(1).gameObject.SetActive(true);

			heartsBoost.transform.GetChild(0).gameObject.SetActive(true);

			boostHearts = 2;

		}

	}



	private void Update()
	{

		if (!GameControl.instance.gameOver)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(horizontalMove * runSpeed, rb.velocity.y);

			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmooth);


			// If the input is moving the player right and the player is facing left...
			if (horizontalMove > 0 && facingRight == false)
			{
				Flip();
			}

			// Otherwise if the input is moving the player left and the player is facing right...
			else if (horizontalMove < 0 && facingRight == true)
			{
				Flip();
			}


			// Check if it is touching the ground
			isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

			if (isGrounded == true)
			{
				animator.SetBool("Jumping", false);
			}

			else
			{
				animator.SetBool("Jumping", true);
			}


			// If A or D are pressed
			// If Left Arrow or Right Arrow are pressed
			if (!GameControl.instance.pauseActive)
			{

				horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

				animator.SetFloat("Movement", Mathf.Abs(horizontalMove));

			}


			// If W or Space Key are pressed

			if (Input.GetButtonDown("Jump") && !GameControl.instance.pauseActive)
			{

				if (isGrounded == true)
				{
					rb.velocity = Vector2.up * jumpForce;

					animator.SetBool("Jumping", true);

					doubleJump = true;

                    if (StateGame.audioState == 1)
                    {

						JumpEffect1.Play();

					}

				}

				// Jump a second time but with less jump force
				else if (doubleJump)
				{
					rb.velocity = Vector2.up * (jumpForce / 1.3f);

					// Pass to idle animation to recreate the jump animation
					animator.Play("PinkGirl_Idle");

					doubleJump = false;

					if (StateGame.audioState == 1)
					{

						JumpEffect2.Play();

					}
					
				}

			}

		}

	}



	private void Flip()
	{

		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}



	public void getDamage()
    {

		// If there is no hearts left
        if (hearts == 0)
        {

			// Disable the left heart
			heartsLeft.transform.GetChild(hearts).gameObject.SetActive(false);

			// Enable the lost heart
			heartsLost.transform.GetChild(hearts).gameObject.SetActive(true);

			hearts--;

			animator.SetTrigger("Death");

			GameControl.instance.CharacterDead();

		}

        else
        {

			if (StateGame.audioState == 1)
			{

				HittedEffect.Play();

			}

			if (boostHearts != 0)
			{

				// Disable the left heart
				heartsBoost.transform.GetChild(boostHearts - 1).gameObject.SetActive(false);

				boostHearts--;

			}

            else
            {

				// Disable the left heart
				heartsLeft.transform.GetChild(hearts).gameObject.SetActive(false);

				// Enable the lost heart
				heartsLost.transform.GetChild(hearts).gameObject.SetActive(true);

				hearts--;

            }
			
		}

	}



    private void OnTriggerEnter2D(Collider2D collision)
    {

		if (collision.gameObject.CompareTag("Coin"))
		{

			Destroy(collision.gameObject);

			coinsCollected++;

			coinsAmount_Text.text = coinsCollected.ToString();

			if (StateGame.audioState == 1)
			{

				CoinEffect.Play();

			}
			
		}

		if (collision.gameObject.CompareTag("Heart"))
		{

			// If there is max health
			if (hearts == 2)
			{

				infoText.text = "Max Health";

				infoText.gameObject.SetActive(true);

			}

			else
			{

				hearts++;

				// Enable the left heart
				heartsLeft.transform.GetChild(hearts).gameObject.SetActive(true);

				// Disable the lost heart
				heartsLost.transform.GetChild(hearts).gameObject.SetActive(false);

				Destroy(collision.gameObject);

				if (StateGame.audioState == 1)
				{

					CollectEffect.Play();

				}

			}
			
		}


		if (collision.gameObject.CompareTag("Key"))
		{

			Destroy(collision.gameObject);

			keyCollected = true;

			StartCoroutine(temporalText("Key collected!"));

			if (StateGame.audioState == 1)
			{

				CollectEffect.Play();

			}

		}


		if (collision.gameObject.CompareTag("Rune"))
		{

			Destroy(collision.gameObject);

			StateGame.runeCollected = true;

			StartCoroutine(temporalText("Boost for boss fight!"));

			StateGame.runeCollected = true;

			if (StateGame.audioState == 1)
			{

				CollectEffect.Play();

			}

		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{

		if (collision.gameObject.CompareTag("Heart"))
		{

			infoText.gameObject.SetActive(false);

		}

	}


	// Coroutine for show a temporal info text
	IEnumerator temporalText(string text)
	{

		// Show text
		infoText.text = text;

		infoText.gameObject.SetActive(true);

		// Wait seconds
		yield return new WaitForSeconds(3);

		// Hide text
		infoText.gameObject.SetActive(false);

	}

}