using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Attack : MonoBehaviour
{

    public Animator animator;

	public AudioSource attackEffect1;
	public AudioSource attackEffect2;

	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask bossLayer;
	public LayerMask enemyLayers;
	public LayerMask boxesLayers;


	void Update()
    {

		if (!GameControl.instance.gameOver)
        {

            if (!GameControl.instance.gameEnd)
            {

				// If 1 or Left Mouse are pressed
				if (Input.GetButtonDown("Attack1") && !GameControl.instance.pauseActive)
				{

					Attack(1);

					if (StateGame.audioState == 1)
					{

						attackEffect1.Play();

					}

				}

				// If 2 or Right Mouse are pressed
				else if (Input.GetButtonDown("Attack2") && !GameControl.instance.pauseActive)
				{

					Attack(2);

					if (StateGame.audioState == 1)
					{

						attackEffect2.Play();

					}

				}

				// If 3 or Wheel Mouse are pressed
				else if (Input.GetButtonDown("Attack3") && !GameControl.instance.pauseActive)
				{

					Attack(3);

					if (StateGame.audioState == 1)
					{

						attackEffect1.Play();

					}

				}

			}
			
		}

	}


    private void Attack(int type)
    {

		// Play attack animation
		animator.SetTrigger("Attack");

		animator.SetInteger("TypeAttack", type);


		// BOSS //

		Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, bossLayer);

		if (colInfo != null)
		{

			colInfo.GetComponent<Boss_Controller>().getHitted();

		}


		// ENEMIES //

		// Detect enemies in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {

			enemy.GetComponent<Enemy_Controller>().getHitted();

        }


		// BOXES //

		// Detect boxes in range of attack
		Collider2D[] hitBoxes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, boxesLayers);

		// Damage them
		foreach (Collider2D box in hitBoxes)
		{

			box.GetComponent<Object_Controller>().getHitted();

		}

	}


    private void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
        {
			return;
        }

		Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

}
