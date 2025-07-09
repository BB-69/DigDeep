using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements;

public class DropItemSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D colliders;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attractRadius = 3f;
    [SerializeField] private float collectDistance = 0.3f;

    public DropItem dropItem;
      private Transform player;
    private bool isCollecting = false;

    void Start()
    {
        sr.sprite = dropItem.image;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure your player has tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null || isCollecting) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attractRadius)
        {
            // เคลื่อนเข้าหา Player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (distance <= collectDistance)
            {
                isCollecting = true;

                bool canAdd = InventoryManager.instance.AddItem(dropItem);
                if (canAdd)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Initialize(DropItem dropItem)
    {
        Debug.Log("Initialize");
        this.dropItem = dropItem;
        sr.sprite = dropItem.image;
    }

/*    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        DropItem myDropItem = GetComponent<DropItemSystem>().dropItem;

        if (myDropItem != null)
        {
             bool canAdd = InventoryManager.instance.AddItem(dropItem);
            if (canAdd)
            {
                StartCoroutine(MoveAndCollect(other.transform));
            }
        }
        else
        {
            Debug.LogError(" no DropItem in setting DropItemSystem!");
        }
         
        }
    }*/

/*    private IEnumerator MoveAndCollect(Transform target)
    {
        Debug.Log("MoveAndCollect");
        Destroy(colliders);

        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return 0;
        }
        Destroy(gameObject);
    }
*/
}
