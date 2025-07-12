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
    [SerializeField] private float attractRadius = 1f;
    //[SerializeField] private float collectDistance = 0.3f;

    public DropItem dropItem;
    private bool isCollecting = false;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        sr.sprite = dropItem.image;
    }

    void Update()
    {
        if (isCollecting) return;
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.transform.position);
        if (distance > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerManager.instance.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCollecting = true;
            PlayerManager.instance.inventory.AddItem(dropItem.itemName, 1);
            PlayerManager.instance.AddXp(dropItem.xp);
            SoundManager.Instance.PlaySFX("Dig");
            Destroy(gameObject);
        }
    }
}
