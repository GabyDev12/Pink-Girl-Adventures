using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

	public float speed = 2.5f;
	public float attackRange = 2f;

	Transform character;
	Rigidbody2D rb;
	Boss_Controller boss;

	int typeAttack = 1;


	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		character = GameObject.FindGameObjectWithTag("Player").transform;

		rb = animator.GetComponent<Rigidbody2D>();

		boss = animator.GetComponent<Boss_Controller>();

		animator.SetBool("Running", true);

	}


	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

        if (!GameControl.instance.gameOver)
        {

			if (!GameControl.instance.pauseActive)
			{

				boss.LookAtPlayer();

				Vector2 target = new Vector2(character.position.x, rb.position.y);
				Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

				rb.MovePosition(newPos);


				// Attack
				if (Vector2.Distance(character.position, rb.position) <= attackRange)
				{

					// Attack animation
					if (typeAttack == 1)
					{

						animator.SetInteger("AttackType", typeAttack);

						typeAttack++;

					}

					else
					{

						animator.SetInteger("AttackType", typeAttack);

						typeAttack--;

					}

					animator.SetTrigger("Attack");

					// Wait after the attack
					boss.doRecovering();

				}

			}

		}
        
	}


	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		animator.ResetTrigger("Attack");

	}

}
