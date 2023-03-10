using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;

	public float speed = 5f;
	public float jumpforce = 5f;
	private bool isGround = false;
	private Vector3 rotation;

	private bool isRunning = false;

	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;



	private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		rotation = transform.eulerAngles;
	}

	private void Update()
	{
		/*if (!m_grounded)
		{
			m_grounded = true;
			
		}

		//Check if character just started falling
		if (m_grounded)
		{
			m_grounded = false;
		}
		// Check for movement input
		float moveHorizontal = Input.GetAxis("Horizontal");
		if (moveHorizontal != 0)
		{
			// Move the character horizontally
			rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
			isRunning = true;

			// Flip the character sprite if moving left
			Vector3 characterScale = transform.localScale;
			if (moveHorizontal < 0)
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
		}

		// Check for jump input
		if (Input.GetKeyDown("space") && m_grounded)
		{
			animator.SetTrigger("Jump");
			m_grounded = false;
			animator.SetBool("Grounded", m_grounded);
			rb.velocity = new Vector2(rb.velocity.x, m_jumpForce);
			// Jump the character
			*//*rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);*//*
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

		if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
		{
			rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
			animator.SetTrigger("Jump");
			isGround = false;
		}
		

		if (Input.GetKeyDown(KeyCode.Z))
		{
			animator.SetTrigger("isAttacking1");
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

			foreach(Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<EnemyController>().TakeDamage(25);
			}
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			animator.SetTrigger("isAttacking2");
		}
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
		animator.SetBool("isRunning", isRunning);
	}
}
