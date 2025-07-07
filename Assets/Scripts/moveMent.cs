using UnityEngine;

public class moveMent : MonoBehaviour
{
    private Rigidbody2D rb;

    public float MoveX , MoveSpeed = 5f , dashSpeed = 20f, dashDuration = 0.2f , JumpForce = 10f, fallMultiply = 2.5f, lowJumpMultiply = 2f;

    private bool isDashing = false, canJump = true;
    private Vector2 Charactor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveX = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)// walk or dash
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump) // jump
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, JumpForce);
            canJump = false;
        }
        if (rb.linearVelocityY < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiply - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocityY > 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiply - 1) * Time.deltaTime;
        }

    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(MoveX * MoveSpeed, rb.linearVelocity.y); // Normal walk 
        }
    }
    System.Collections.IEnumerator Dash() //Dash
    {
        isDashing = true;
        rb.linearVelocity = new Vector2(MoveX * dashSpeed, rb.linearVelocity.y);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
}
