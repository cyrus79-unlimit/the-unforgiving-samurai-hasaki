	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;

	public float speed = 7f;
	public float jumpforce = 7f;
	private bool isGround = false;
	private Vector3 rotation;

	private bool isRunning = false;

	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;

	// Player life
	public int healthPlayer;
	public int maxHealthPlayer = 2;

	public GameOverScreen GameOverScreen;
	public float uiDeplay = 0.1f;

	public GameObject[] hearts;

	public void GameOver()
	{		
		GameOverScreen.SetUp();
	}


	private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		rotation = transform.eulerAngles;

		// Set health to player
		healthPlayer = maxHealthPlayer;

	}

	private void Update()
	{	
		if(healthPlayer < 1)
		{
			Destroy(hearts[1].gameObject);
		} else if (healthPlayer < 2)
		{
			Destroy(hearts[0].gameObject);
		}

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
			isRunning = false;
			isGround = false;
		}
		

		if (Input.GetKeyDown(KeyCode.Z))
		{
			animator.SetTrigger("isAttacking1");
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

			foreach(Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<Bandit_Bot>().TakeDamage(25);
			}
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			animator.SetTrigger("isAttacking2");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Bandit_Bot>().TakeDamage(25);
            }
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
        if (collision.gameObject.name.Contains("wall"))
        {
			GameOver();
        }
    }

	public void DamagedPlayer(int damage)
    {
		healthPlayer -= damage;
		// Add animation Hurt below
		animator.SetTrigger("isHurt");
		// ========================
		if(healthPlayer <= 0)
        {
            // Add animation die below
            animator.SetTrigger("isDead");
			// =======================	
			Invoke("GameOver",uiDeplay);
        }
	}

	private void FixedUpdate()
	{
		// Update the animator based on the character's movement state
		animator.SetBool("isRunning", isRunning);
	}
}
