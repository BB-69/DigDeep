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
    public BombThrower bombThrower;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
    }

    public void AddXp(int amount)
    {
        GameManager.instance.AddXp(amount);
        xp += amount;
    }

    public void ResetXP()
    {
        xp = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            Debug.Log(collision.gameObject.name);
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