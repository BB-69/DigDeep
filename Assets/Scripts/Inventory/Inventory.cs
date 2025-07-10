using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inventory;

    protected virtual void Awake()
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

    public virtual void AddItem(string itemName, int amount)
    {
        if (PlayerHasKey(itemName))
        {
            this.inventory[itemName] += amount;
        }
        else
        {
            this.inventory.Add(itemName, amount);
        }
    }

    public virtual bool PlayerHasKey(string itemName)
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

    public virtual bool PlayerHasItem(string itemName, int amount)
    {
        return this.inventory.ContainsKey(itemName) && inventory[itemName] >= amount;
    }

    public virtual bool MinusItem(string itemName, int amount)
    {
        if (PlayerHasItem(itemName, amount))
        {
            this.inventory[itemName] -= amount;
            return true;
        }
        return false;
    }

    public virtual void TransferItemTo(Inventory another, string itemName)
    {
        if (PlayerHasKey(itemName))
        {
            another.AddItem(itemName, this.inventory[itemName]);
            this.inventory[itemName] = 0;
        }
    }

    public virtual void TransferItemTo(Inventory another, string itemName, int amount)
    {
        if (PlayerHasItem(itemName, amount))
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
        if (this.inventory != null && PlayerHasKey(ItemName))
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
