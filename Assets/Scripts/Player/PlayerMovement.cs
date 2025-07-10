using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float MoveX, MoveSpeed = 1f, dashSpeed = 5f, dashDuration = 0.2f, JumpForce = 10f, groundCheckDistance = 0.2f;

    public int maxJump = 2, jumpCount = 0;

    public LayerMask groundLayer;

    public Bomb LaunchableProjectile;

    public Transform LaunchOffset, groundCheckPoint;

    private bool isDashing = false, wasGroundedLastFrame = false;
    private Vector2 Charactor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveX = Input.GetAxis("Horizontal");
        transform.position += new Vector3(MoveX, 0, 0) * Time.deltaTime * MoveSpeed;

        if (!Mathf.Approximately(0, MoveX))
            transform.rotation = MoveX > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

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

        if (Input.GetButtonDown("Fire1")) //shoot bomb
        {


            Instantiate(LaunchableProjectile, LaunchOffset.position, LaunchOffset.rotation);
        }

    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(MoveX * MoveSpeed, rb.linearVelocity.y); // Normal walk 
        }
    }
    System.Collections.IEnumerator Dash()
    {
        isDashing = true;

        float dashTime = 0f;
        while (dashTime < dashDuration)
        {
            transform.position += new Vector3(MoveX, 0, 0) * dashSpeed * Time.deltaTime;
            dashTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}
