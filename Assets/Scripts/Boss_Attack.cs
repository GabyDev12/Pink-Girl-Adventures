using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{

	public Transform attackPoint;
	public float attackRange = 1f;
	public LayerMask characterLayer;

	public AudioSource AttackEffect;

	private Character_Controller sciptCharacter;


	void Start()
	{

		sciptCharacter = GameObject.Find("Character").GetComponent<Character_Controller>();

	}


	public void Attack()
	{

		if (StateGame.audioState == 1)
		{

			AttackEffect.Play();

		}

		Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, characterLayer);

		if (colInfo != null)
		{

            if (!GameControl.instance.gameEnd)
            {

				sciptCharacter.getDamage();

            }

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
