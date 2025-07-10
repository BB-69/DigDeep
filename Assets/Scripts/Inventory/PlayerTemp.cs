using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTemp : MonoBehaviour
{
    public Inventory inventory { get;  private set; }
    public float xp { get;  private set; }
    IInteractable interactableObj;
    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactableObj!=null)
            {
                interactableObj.Interact(this);
            }
        }
    }

    public void AddXp(int amount)
    {
        xp += amount;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Interactable"))
            interactableObj = collision.GetComponent<IInteractable>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Interactable"))
            interactableObj = collision.GetComponent<IInteractable>();
    }
}