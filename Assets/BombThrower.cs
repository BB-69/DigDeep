using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bombPrefab;
    public int bombCount = 3;
    public DungeonManager dungeonManager;
     void Start()
    {
      
    if (dungeonManager == null)
        dungeonManager = FindObjectOfType<DungeonManager>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bombCount > 0)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector2 direction = (mouseWorldPos - transform.position).normalized;

            GameObject bombObj = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            Bomb bomb = bombObj.GetComponent<Bomb>();
            bomb.Launch(direction, dungeonManager);

            bombCount--;
        }
    }
}
