using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    // Player variables
    public float moveSpeed;
    public float jumpHeight;
    public LayerMask floorLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        Movement();

        

    }

    void Movement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // Flip the sprite when moving left or right
        if (moveInput < 0)
        {
            // Flip the sprite 
            transform.localScale = new Vector3(-6.210505f, 6.210505f, 6.210505f); 
        }
        else if (moveInput > 0)
        {
            // Flip back
            transform.localScale = new Vector3(6.210505f, 6.210505f, 6.210505f); 
        }


        // Target velocity should be the maxSpeed or the direction the player is moving.
        float targetVelocityX = moveSpeed * moveInput;

        // deccelerate if no input
        if (moveInput == 0)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, moveSpeed), rb.velocity.y);
        }
        else
        {
            // accelerate 
            float newVelocityX = Mathf.MoveTowards(rb.velocity.x, targetVelocityX, moveSpeed);
            rb.velocity = new Vector2(newVelocityX, rb.velocity.y);
        }

        
        

        Debug.Log("X Velocity: " + rb.velocity.x + "|  Y Velocity: " + rb.velocity.y);
    }

    void Jump()
    {
        
        
            // Apply upward force for the jump
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        
        
    }

    // Detect collisions with objects and check if they have the correct tag
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the "Ground" tag
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            Debug.Log("Collided with ground!");
        }
    }

    // Optionally, handle when the player leaves the ground
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
            Debug.Log("Left the ground!");
        }
    }

}
