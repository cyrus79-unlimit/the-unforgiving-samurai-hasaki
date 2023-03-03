using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2(horizontalInput, verticalInput);
		movement = Vector2.ClampMagnitude(movement, 1f);

		float speed = 10f;
		Vector2 velocity = movement * speed;

		float friction = 0.9f;
		rb.velocity *= friction;
		rb.AddForce(velocity, ForceMode2D.Impulse);
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
