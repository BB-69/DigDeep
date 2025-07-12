using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    private Rigidbody2D rb;

    public float MoveX, MoveSpeed = 1f, dashSpeed = 5f, dashDuration = 0.2f, JumpForce = 10f, groundCheckDistance = 0.2f;

    public int maxJump = 2, jumpCount = 0;

    public LayerMask groundLayer;
    public Transform LaunchOffset, groundCheckPoint;

    private bool isDashing = false, wasGroundedLastFrame = false;
    public float fallMultiplier = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;
        
        if (transform.position.y < -20f)
        {
            canMove = false;
            GameManager.instance.LoseGame();
        }

        MoveX = Input.GetAxis("Horizontal");

        bool isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);

        if (isGrounded && !wasGroundedLastFrame) jumpCount = 0;

        wasGroundedLastFrame = isGrounded;

        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            jumpCount++;
            Debug.Log(jumpCount);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)// walk or dash
        {
            StartCoroutine(Dash());
        }

    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(MoveX * MoveSpeed, rb.linearVelocity.y); // Only set X velocity
        }
        // Apply extra gravity when falling
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * rb.mass);
        }
    }
    System.Collections.IEnumerator Dash()
    {
        isDashing = true;

        float dashX = Input.GetAxis("Horizontal");
        float dashY = Input.GetAxis("Vertical");
        Vector2 dashDir = new Vector2(dashX, dashY).normalized;

        if (dashDir == Vector2.zero)
            dashDir = new Vector2(MoveX, 0).normalized;

        rb.linearVelocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = new Vector2(MoveX * MoveSpeed, rb.linearVelocityY); // Restore normal movement
        isDashing = false;
    }
}
