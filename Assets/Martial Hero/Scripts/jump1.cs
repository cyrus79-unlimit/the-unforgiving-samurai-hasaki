using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class jump1 : MonoBehaviour
{
    public float speed = 5f;
    public float jumpforce = 5f;
    private Rigidbody2D rb;
    private bool isGround = false;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        if(xMove  < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector2.left * speed * -xMove * Time.deltaTime);
        }else if(xMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector2.right * speed * xMove * Time.deltaTime);
        }
        if(isGround == false)
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
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "ground")
        {
            isGround = true;
        }
    }

    
}
