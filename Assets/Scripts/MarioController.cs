using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for reloading the level

public class MarioController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public LayerMask floorLayer;
    public Animator animator;
    public GameObject DeadMarioObject;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private bool isDead = false; // Track if Mario is dead
    private bool isPoweredUp = false;
    private float moveInput;

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

        
        moveInput = Input.GetAxis("Horizontal");

        
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (isDead) return;

        

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        float targetVelocityX = moveSpeed * moveInput;

        if (moveInput == 0)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, moveSpeed), rb.velocity.y);
            SoundManager.instance.StopWalkSound();
        }
        else
        {
            float newVelocityX = Mathf.MoveTowards(rb.velocity.x, targetVelocityX, moveSpeed);
            rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

            if (isGrounded && !isJumping)
            {
                SoundManager.instance.PlayWalkSound();
            }
            else
            {
                SoundManager.instance.StopWalkSound();
            }
        }

        if (Mathf.Abs(moveInput) < 0.1f && Mathf.Abs(rb.velocity.x) < 0.1f)
        {
            
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        Debug.Log($"Velocity: {rb.velocity.x}, Input: {moveInput}");
    }

    void Jump()
    {
        SoundManager.instance.StopWalkSound();
        SoundManager.instance.PlayJumpSound();

        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        isJumping = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Allow Mario to jump from different solid objects
        if (collision.gameObject.CompareTag("Floor") || 
            collision.gameObject.CompareTag("Wall") || 
            collision.gameObject.CompareTag("Question Mark") || 
            collision.gameObject.CompareTag("Stair") || 
            collision.gameObject.CompareTag("Tube Head"))
        {
            isGrounded = true;
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        // Big Mario can break certain objects when powered up
        if (collision.gameObject.CompareTag("Wall") && isPoweredUp)
        {
            Destroy(collision.gameObject);
        }

        // Handle enemy collision
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ContactPoint2D[] contacts = collision.contacts;
            foreach (ContactPoint2D contact in contacts)
            {
                if (contact.normal.y > 0.5f) // Mario lands on top of an enemy
                {
                    SoundManager.instance.PlayStompSound();
                    Destroy(collision.gameObject); // Remove enemy
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight); // Mario bounces
                    return;
                }
            }

            // If Mario is powered up, he loses power instead of dying
            if (isPoweredUp)
            {
                LosePowerUp();
            }
            else
            {
                Die(); // Mario dies only if not from the top
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Ensure Mario isn't considered grounded when leaving a solid object
        if (collision.gameObject.CompareTag("Floor") || 
            collision.gameObject.CompareTag("Wall") || 
            collision.gameObject.CompareTag("Question Mark") || 
            collision.gameObject.CompareTag("Stair") || 
            collision.gameObject.CompareTag("Tube Head"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            SoundManager.instance.PlayPickupSound();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("DeathZone"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("Entrance"))
        {
            SoundManager.instance.PlayClearStageSound();
            rb.transform.position = new Vector2(50.68497f, -40.18793f);
        }

        if (other.gameObject.CompareTag("Exit"))
        {
            rb.transform.position = new Vector2(153f, -0.5f);
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        //animator.SetTrigger("Die");
        animator.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(DeadMarioObject, transform.position, Quaternion.identity);


        SoundManager.instance.StopWalkSound();
        SoundManager.instance.PlayDeathSound();

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(RestartLevel());
    }

    public void ReceivePowerUp()
    {
        isPoweredUp = true;

        //temporary in place for the big mario
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("IsSuperMario", true);
    }

    private void LosePowerUp()
    {
        isPoweredUp = false;

        //temporary in place for the big mario
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("IsSuperMario", true);

    }   

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
