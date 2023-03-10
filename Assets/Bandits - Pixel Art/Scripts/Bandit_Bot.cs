using UnityEngine;
using System.Collections;

public class Bandit_Bot : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;   
    public int health;
    public int maxHealth = 25;
    public CharacterController playerhealth;
    public int damage = 1;


    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;   

    // Timer
    const float TimerDuration = 1;
    Timer directionTimer;
    int directionMultiplier = 1;

    // Flag to indicate if the bot is moving left
    // private bool isMovingLeft = false;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        health = maxHealth;

        // add and run the timer
        directionTimer = gameObject.AddComponent<Timer>();
        directionTimer.AddTime(TimerDuration);
        directionTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            // Bandit play animation run
            m_animator.SetInteger("AnimState", 2);

            // Change direction as appropriate
            if (directionTimer.Finished)
            {
                // move the opposite when finish time
                directionMultiplier *= -1;
                directionTimer.Run();
            }

            // Move the bot
            // m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
            transform.Translate(Vector2.right * m_speed * directionMultiplier * Time.deltaTime);

            // Check Bandit move left or right to flip
            if (directionMultiplier < 0)
            {
                // Bandit is facing left
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else if (directionMultiplier > 0)
            {
                // Bandit is facing right
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        /*m_animator.SetTrigger("Hurt");*/
        m_animator.SetTrigger("Hurt");     
        if (health <= 0)
        {
            m_animator.SetTrigger("Death");
            GetComponent<BoxCollider2D>().enabled = false;
            this.enabled = false;
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerhealth.DamagedPlayer(damage);
        }
    }

}

