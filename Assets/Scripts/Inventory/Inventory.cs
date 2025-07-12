using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inventory;

    void Awake()
    {
        if (this.inventory == null)
        {
            this.inventory = new Dictionary<string, int>()
            { { "Copper", 0 }, { "Iron", 0 }, { "Gold", 0 }, { "Emerald", 0 } };
        }
    }

    void OnEnable()
    {
        GameManager.OnLevelStart += ClearInventory;
    }

    void OnDisable()
    {
        GameManager.OnLevelStart -= ClearInventory;
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }

    public void AddItem(string itemName, int amount)
    {
        if (IsPlayerHasItem(itemName))
        {
            this.inventory[itemName] += amount;
        }
        else
        {
            this.inventory.Add(itemName, amount);
        }
        UIManager.Instance.UpdateInventoryUI(this.inventory);
    }

    public bool IsPlayerHasItem(string itemName)
    {
        if (this.inventory == null)
        {
            return false;
        }
        else
        {
            return this.inventory.ContainsKey(itemName);
        }
    }

    public bool IsPlayerHasEnoughItem(string itemName, int amount)
    {
        return this.inventory.ContainsKey(itemName) && inventory[itemName] >= amount;
    }

    public bool RemoveItem(string itemName, int amount)
    {
        if (IsPlayerHasEnoughItem(itemName, amount))
        {
            this.inventory[itemName] -= amount;
            UIManager.Instance.UpdateInventoryUI(this.inventory);
            return true;
        }
        return false;
    }

    public void TransferItemTo(Inventory another, string itemName)
    {
        if (IsPlayerHasItem(itemName))
        {
            another.AddItem(itemName, this.inventory[itemName]);
            this.inventory[itemName] = 0;
            UIManager.Instance.UpdateInventoryUI(this.inventory);
        }
    }

    public virtual void TransferItemTo(Inventory another, string itemName, int amount)
    {
        if (IsPlayerHasEnoughItem(itemName, amount))
        {
            another.AddItem(itemName, amount);
            this.inventory[itemName] -= amount;
            UIManager.Instance.UpdateInventoryUI(this.inventory);
        }
        //inventory.Clear();
    }

    public virtual void TransferAll(Inventory another)
    {
        foreach (var item in inventory.ToList())
        {
            another.AddItem(item.Key, item.Value);
            this.inventory[item.Key] = 0;
            UIManager.Instance.UpdateInventoryUI(this.inventory);
        }
    }

    public virtual int Get(string ItemName)
    {
        if (this.inventory != null && IsPlayerHasItem(ItemName))
        {
            return this.inventory[ItemName];
        }
        return 0;
    }

    public override string ToString()
    {
        string result = "Inventory Contents:\n";
        foreach (var item in this.inventory)
        {
            result += $"{item.Key}: {item.Value} ";
        }
        return result;
    }
}
