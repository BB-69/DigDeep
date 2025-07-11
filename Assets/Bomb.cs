using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    public float speed = 10f;
    public float explodeRadius = 1.5f;
    public float explodeDelay = 1.5f;
    public int damage = 50;
    private Rigidbody2D rb;
    private Vector2 direction;
    private DungeonManager dungeonManager;
    public Tilemap tilemap;
    public GameObject explosionEffect; // Prefab ของ VFX


    void Start()
    {
        if (tilemap == null)
        tilemap = FindObjectOfType<Tilemap>();

    if (dungeonManager == null)
        dungeonManager = FindObjectOfType<DungeonManager>();
    }

    public void Launch(Vector2 dir, DungeonManager manager)
    {
        rb = GetComponent<Rigidbody2D>();
        direction = dir.normalized;
        rb.linearVelocity = direction * speed;
        dungeonManager = manager;
        Invoke("Explode", explodeDelay); // ระเบิดหลังจาก 1.5 วิ
    }

    void Explode()
    {
        
          if (explosionEffect != null)
        {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Debug.Log("Explosion VFX spawned");
        }
         else
        {
        Debug.LogWarning("ExplosionEffect prefab is not assigned!");
        }
 
        Vector3 pos = transform.position;
        Vector3Int centerCell = tilemap.WorldToCell(pos);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explodeRadius);

        foreach (var hit in hits)
        {
            if (hit == null || hit.transform == null)
            continue; // ข้าม object ที่ถูกลบไปแล้ว
            Vector3 worldPos = hit.transform.position;
            Vector3Int cell = dungeonManager.tilemap.WorldToCell(worldPos);
           dungeonManager.TakeDamageBlock(cell, damage);
                
            
            
        }
        

        // TODO: Add explosion VFX
        
        //Destroy(gameObject);
    }
}
