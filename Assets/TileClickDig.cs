using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClickDig : MonoBehaviour
{
    public Transform player;
    public GameObject pickaxe; // ตัว Pickaxe ที่มี Animator
    public Tilemap tilemap;
    public DungeonManager dungeonManager;
    public float maxDistance = 5f;
    public Camera mainCamera;

    private Animator pickaxeAnimator;
    private bool isSwinging = false;

    void Start()
    {
        if (player == null)
    {
        GameObject found = GameObject.FindWithTag("Player");
        if (found != null)
            player = found.transform;
        else
            Debug.LogError("Player not found!");
    }

      if (pickaxe == null)
        pickaxe = GameObject.Find("Pickaxe"); // หรือใช้ tag ก็ได้

    if (tilemap == null)
        tilemap = FindObjectOfType<Tilemap>();

    if (dungeonManager == null)
        dungeonManager = FindObjectOfType<DungeonManager>();

    pickaxeAnimator = pickaxe?.GetComponent<Animator>();
    if (pickaxe != null) pickaxe.SetActive(false);
            
    }

    void Update()
    {
       if (player == null)
    {
        GameObject found = GameObject.FindWithTag("Player");
        if (found != null)
        {
            player = found.transform;
            pickaxe = GameObject.Find("Pickaxe"); // หรือค้นจาก child ของ player ก็ได้
            pickaxeAnimator = pickaxe?.GetComponent<Animator>();
            if (pickaxe != null) pickaxe.SetActive(false);
        }
        else
        {
            // ยังหาไม่เจอ → รอรอบถัดไป
            return;
        }
    }
    
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPos);

            float distance = Vector3.Distance(player.position, cellCenter);
               Debug.Log("Mouse Position (Screen): " + Input.mousePosition);
                Debug.Log("Mouse World Position: " + mouseWorldPos);
                Debug.Log("Tile Cell Clicked: " + cellPos);
            if (distance <= maxDistance)
            {
                StartCoroutine(SwingPickaxe(cellCenter));
                dungeonManager.TakeDamageBlock(cellPos, 15);
                Debug.Log("Dealing damage to block");
            }
            else
            {
                Debug.Log("Too far to dig");
            }
        }
    }

    System.Collections.IEnumerator SwingPickaxe(Vector3 targetPos)
    {
        isSwinging = true;
        pickaxe.SetActive(true);

        // คำนวณตำแหน่งขวานให้ใกล้ Player และหันไปทางเป้าหมาย
        Vector3 dir = (targetPos - player.position).normalized;
        Vector3 offset = dir * 0.8f;
       pickaxe.transform.position = player.position + offset;

        if (dir.x < 0)
            pickaxe.transform.localScale = new Vector3(-1, 1, 1); // หันซ้าย
        else
            pickaxe.transform.localScale = new Vector3(1, 1, 1);  // หันขวา

       // pickaxeAnimator.SetTrigger("Swing");

        yield return new WaitForSeconds(0.2f); // ระยะเวลา animation

        pickaxe.SetActive(false);
        isSwinging = false;
    }
}
