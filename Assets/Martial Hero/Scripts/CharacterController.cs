using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;
	private bool isRunning = false;

	public float moveSpeed = 10f;
	public float jumpForce = 10f;

	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
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
		if (Input.GetButtonDown("Jump"))
		{
			// Jump the character
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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

	private void FixedUpdate()
	{
		// Update the animator based on the character's movement state
		animator.SetBool("isRunning", isRunning);
		animator.SetFloat("isJumping", rb.velocity.y);
	}
}
