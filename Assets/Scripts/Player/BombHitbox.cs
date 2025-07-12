using UnityEngine;

public class BombHitbox : MonoBehaviour
{
    // attach this to explode effect
    public int damage = 50;
    Rigidbody2D rb;
    [SerializeField] float explodeRadius = 4;
    [SerializeField] float bounceForce = 20f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void explode()
    {
        Vector3 pos = transform.position;
        Vector3Int centerCell = DungeonManager.instance.tilemap.WorldToCell(pos);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explodeRadius);

        foreach (var hit in hits)
        {
            if (hit == null || hit.transform == null)
                continue; // ข้าม object ที่ถูกลบไปแล้ว
            if (hit.CompareTag("Player"))
            {
                Rigidbody2D playerRb = hit.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 bounceDir = (hit.transform.position - transform.position).normalized;
                    playerRb.AddForce(bounceDir * bounceForce, ForceMode2D.Impulse);
                }

            }
            else
            {
                Vector3 worldPos = hit.transform.position;
                Vector3Int cell = DungeonManager.instance.tilemap.WorldToCell(worldPos);
                DungeonManager.instance.TakeDamageBlock(cell, damage);
            }
        }
    }
    public void Finish()
    {
        Debug.Log("BombHitbox finished");
        BombThrower.OnFinishThrow?.Invoke();
    }
}