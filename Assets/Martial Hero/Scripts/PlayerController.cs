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
			isGround = false;		}
		
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
	}
}
