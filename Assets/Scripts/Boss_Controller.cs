using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{

	public Animator animator;

	public Transform character;

	public AudioSource HittedEffect;

	private bool isFlipped = false;


	// Health
	private int maxHealth = 20;
	private int currentHealth;

	public Boss_HealtBar healtBar;


	private void Start()
    {

		currentHealth = maxHealth;

		healtBar.SetMaxHealth(maxHealth);

    }


    public void LookAtPlayer()
	{

		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > character.position.x && isFlipped)
		{

			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);

			isFlipped = false;

		}

		else if (transform.position.x < character.position.x && !isFlipped)
		{

			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;

		}

	}


	public void getHitted()
	{

		animator.SetTrigger("TakeHitted");

		if (StateGame.audioState == 1)
		{

			HittedEffect.Play();

		}

		// If there is no hearts left
		if (currentHealth == 0)
		{

			animator.SetTrigger("Death");

			GetComponent<Collider2D>().enabled = false;

			GameControl.instance.BossDead();

		}

		else
		{

			currentHealth--;

			healtBar.SetHealth(currentHealth);

		}

	}


	public void doRecovering()
    {

		StartCoroutine(bossRecovering());

    }


	// Coroutine for recovering
	IEnumerator bossRecovering()
	{

		// Show text
		animator.SetBool("Recovering", true);

		// Wait seconds
		yield return new WaitForSeconds(3);

		// Hide text
		animator.SetBool("Recovering", false);

	}

}
