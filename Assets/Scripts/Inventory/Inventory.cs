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
            { { "copper", 0 }, { "iron", 0 }, { "gold", 0 }, { "emerald", 0 } };
        }
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
        }
    }

    public virtual void TransferItemTo(Inventory another, string itemName, int amount)
    {
        if (IsPlayerHasEnoughItem(itemName, amount))
        {
            another.AddItem(itemName, amount);
            this.inventory[itemName] -= amount;
        }
        //inventory.Clear();
    }

    public virtual void TransferAll(Inventory another)
    {
        foreach (var item in inventory.ToList())
        {
            another.AddItem(item.Key, item.Value);
            this.inventory[item.Key] = 0;
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
