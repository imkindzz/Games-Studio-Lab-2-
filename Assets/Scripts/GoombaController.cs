using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for reloading the level

public class GoombaMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Goomba movement speed
    private Rigidbody2D rb;
    private bool movingRight = false; // Initial direction to the left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float direction = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Flip direction when colliding with a wall or Mario
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) return; // Keep moving when touching the floor
        if (collision.gameObject.CompareTag("Enemy")) return; // Avoid interaction with other enemies

        // Flip direction when colliding with walls or Mario
        Flip();
    }

    // Detect if Goomba is only half on the floor
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Floor")) return; // Only check the floor

        int leftContacts = 0;
        int rightContacts = 0;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.point.x < transform.position.x) // Contact on the left side
            {
                leftContacts++;
            }
            else if (contact.point.x > transform.position.x) // Contact on the right side
            {
                rightContacts++;
            }
        }

        // Flip direction if Goomba is only half on the floor
        if ((movingRight && rightContacts == 0 && leftContacts > 0) || (!movingRight && leftContacts == 0 && rightContacts > 0))
        {
            Flip();
        }
    }
}
