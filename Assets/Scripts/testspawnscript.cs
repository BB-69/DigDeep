using JetBrains.Annotations;
using UnityEngine;

public class testspawnscript : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public DropItem[] itemsToPickup;

    public void PickUpItem(int id)
    {   

        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Can't add item");
        }
    }
}
