using UnityEngine;
using System.Collections.Generic;


public class InventoryStatTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static InventoryStatTracker instance;

    // Dictionary เก็บสถิติ: [ชื่อไอเทม] = จำนวน
    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // เพิ่มไอเทมเข้าสต็อก
    public void AddItem(string itemName, int amount = 1)
    {
        if (itemCounts.ContainsKey(itemName))
        {
            itemCounts[itemName] += amount;
        }
        else
        {
            itemCounts[itemName] = amount;
        }
    }

    // ลบไอเทมออกจากสต็อก
    public void RemoveItem(string itemName, int amount = 1)
    {
        if (itemCounts.ContainsKey(itemName))
        {
            itemCounts[itemName] = Mathf.Max(0, itemCounts[itemName] - amount);
        }
    }

    // เช็คจำนวนไอเทมจากชื่อ
    public int GetItemCount(string itemName)
    {
        if (itemCounts.TryGetValue(itemName, out int count))
        {
            return count;
        }
        return 0;
    }
}
