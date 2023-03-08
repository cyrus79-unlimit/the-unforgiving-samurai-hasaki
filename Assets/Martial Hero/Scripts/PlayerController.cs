using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	public float jumpforce = 5f;
	private Rigidbody2D rb;
	private bool isGround = false;
	private Vector3 rotation;

	private bool isRunning = false;
	private bool isJumping = false;

	private Animator m_animator;
	private Rigidbody2D m_body2d;

	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;

	// Use this for initialization
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();
		rotation = transform.eulerAngles;
	}

	// Update is called once per frame
	void Update()
	{
		// -- Handle input and movement --
		/*float inputX = Input.GetAxis("Horizontal");
		if(inputX != 0)
		{
			// Move
			m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
			isRunning = true;

			Vector3 characterScale = transform.localScale;
			if (inputX < 0)
			{
				characterScale.x = -1;
			}
			else
			{
				characterScale.x = 1;
			}
			transform.localScale = characterScale;
		}
		else
		{
			isRunning = false;
		}*/
		// Swap direction of sprite depending on walk direction
		/*if (inputX < 0)
		{
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			isRunning = true;
		}	
		else if (inputX > 0)
		{
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			isRunning = true;
		}
		else
		{
			isRunning = false;
		}*/

		float xMove = Input.GetAxis("Horizontal");
		if (xMove < 0)
		{
			isRunning = true;
			transform.localScale = new Vector3(-1, 1, 1);
			transform.Translate(Vector2.left * speed * -xMove * Time.deltaTime);
			
		}
		else if (xMove > 0)
		{
			isRunning = true;
			transform.localScale = new Vector3(1, 1, 1);
			transform.Translate(Vector2.right * speed * xMove * Time.deltaTime);		
		}
		else
		{
			isRunning = false;
		}
		if (isGround == false)
		{
			print("is jump");
		}
		else
		{
			print("NOT jump");
		}
		if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
		{
			rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
			/*isJumping = true;*/
			isGround = false;
		}
		/*else
		{
			isJumping = false;
		}*/


		//Set AirSpeed in animator
		/*m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

		// -- Handle Animations --
		//Attack
		if (Input.GetKeyDown(KeyCode.Z))
		{
			m_animator.SetTrigger("isAttacking1");
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

			foreach (Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<EnemyController>().TakeDamage(25);
			}
		}

		//Jump
		if (Input.GetKeyDown("space") *//*&& m_grounded*//*)
		{
			m_animator.SetTrigger("jump");
			m_grounded = false;
			m_animator.SetBool("Grounded", m_grounded);
			m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
			m_groundSensor.Disable(0.2f);
		}*/
	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;

		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "ground")
		{
			isGround = true;
		}
	}

	private void FixedUpdate()
	{
		// Update the animator based on the character's movement state
		m_animator.SetBool("isRunning", isRunning);
		/*m_animator.SetBool("isJumping", isJumping)*/;
	}
}
