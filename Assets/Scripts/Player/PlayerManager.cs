using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public Inventory inventory { get; private set; }
    public float xp { get;  private set; }
    IInteractable interactableObj;
    void Awake()
    {
        inventory = GetComponent<Inventory>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnEnable()
    {
        GameManager.OnLevelStart += ResetXP ;
    }
    void OnDisable()
    {
        GameManager.OnLevelStart -= ResetXP;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactableObj != null)
            {
                interactableObj.Interact(this);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.AddItem("Copper", 4000);
            inventory.AddItem("Iron", 300);
        }
    }

    public void AddXp(int amount)
    {
        xp += amount;
    }

    public void ResetXP()
    {
        GameManager.instance.AddXp((int)xp);
        xp = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            interactableObj = collision.GetComponent<IInteractable>();
            interactableObj.OnEnterInteractRange();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            interactableObj = collision.GetComponent<IInteractable>();
            interactableObj.OnExitInteractRange();
        }
    }
}