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

    public void GetSelectedItem()
    {
        DropItem receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item");
        }
        else
        {
            Debug.Log("No Item received");
        }
    }

        public void UseGetSelectedItem()
    {
        DropItem receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("use item");
        }
        else
        {
            Debug.Log("No Item use");
        }
    }
}

