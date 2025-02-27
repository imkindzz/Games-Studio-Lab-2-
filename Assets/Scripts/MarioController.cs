using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for reloading the level

public class MarioController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public LayerMask floorLayer;
    public Animator animator;
    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource pickupSound;
    public AudioSource deathSound; // NEW: Death Sound
    public AudioSource stompSound; // NEW: Stomp Sound

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private bool isDead = false; // Track if Mario is dead

    float horizontalMove = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            Jump();
        }

        Movement();
    }

    void Movement()
    {
        if (isDead) return;

        float moveInput = Input.GetAxis("Horizontal");

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-6.210505f, 6.210505f, 6.210505f);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(6.210505f, 6.210505f, 6.210505f);
        }

        float targetVelocityX = moveSpeed * moveInput;

        if (moveInput == 0)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, moveSpeed), rb.velocity.y);
            StopWalkingSound();
        }
        else
        {
            float newVelocityX = Mathf.MoveTowards(rb.velocity.x, targetVelocityX, moveSpeed);
            rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

            if (isGrounded && !isJumping)
            {
                PlayWalkingSound();
            }
            else
            {
                StopWalkingSound();
            }
        }
    }

    void Jump()
    {
        StopWalkingSound();

        if (jumpSound != null)
        {
            jumpSound.Play();
        }

        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        isJumping = true;
    }

    void PlayWalkingSound()
    {
        if (!walkSound.isPlaying)
        {
            walkSound.Play();
        }
    }

    void StopWalkingSound()
    {
        if (walkSound.isPlaying)
        {
            walkSound.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ContactPoint2D[] contacts = collision.contacts;
            foreach (ContactPoint2D contact in contacts)
            {
                if (contact.normal.y > 0.5f) // Mario lands on top of Goomba
                {
                    if (stompSound != null) // Play stomp sound
                    {
                        stompSound.Play();
                    }
                    Destroy(collision.gameObject); // Remove Goomba
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight); // Mario bounces
                    return;
                }
            }
            Die(); // Mario dies only if not from the top
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (pickupSound != null)
            {
                pickupSound.Play();
            }

            //Destroy(other.gameObject); //already destroys the item in the PickUpItems.cs
        }

        if (other.gameObject.CompareTag("DeathZone"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("Entrance"))
        {
            SceneManager.LoadScene("Secret");
        }

        if (other.gameObject.CompareTag("Exit"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        StopWalkingSound();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (deathSound != null)
        {
            deathSound.Play();
        }

        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
