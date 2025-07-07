using JetBrains.Annotations;
using UnityEngine;

public class testspawnscript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public DropItem[] itemsToPickup;

    public void PickUpItem(int id)
    {
        inventoryManager.AddItem(itemsToPickup[id]);
    }
}
