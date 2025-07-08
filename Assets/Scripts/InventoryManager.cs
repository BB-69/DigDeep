using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;

    public bool isOnCheckpoint = true;
    public int maxStackedItems = 20;
   
    public GameObject inventoryItemPrefab;
    
    public bool AddItem(DropItem dropItem)
    {
        for (int i = 0; i < 24; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.dropItem == dropItem && itemInSlot.count < maxStackedItems)
                {
                    itemInSlot.count++;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }
        if (isOnCheckpoint == true)
        {
            for (int i = 0; i < 24; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItem(dropItem, slot);
                    return true;
                }
            }

        }
        //find empty slot
        for (int i = 0; i < 6; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(dropItem, slot);
                return true;
            }

        }
        return false;
    }
    void SpawnNewItem(DropItem dropItem, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(dropItem);
    }
}
