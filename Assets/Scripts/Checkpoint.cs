using System;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
    public bool cleared = false;
    public bool isSpawnpoint = false;
    Dictionary<string, int> oreNeeds = new Dictionary<string, int>();

    public void Setup(int copper, int iron, int gold, int emerald)
    {
        oreNeeds["copper"] = copper;
        oreNeeds["iron"] = iron;
        oreNeeds["gold"] = gold;
        oreNeeds["emerald"] = emerald;

        Debug.Log($"Need {oreNeeds["copper"]} {oreNeeds["iron"]} {oreNeeds["gold"]} {oreNeeds["emerald"]}");
    }

    public void Interact(PlayerTemp playerTemp)
    {
        if (isSpawnpoint || cleared ) return;
        CheckIfCanPass(playerTemp.inventory);
        //condition check if can go
    }

    void CheckIfCanPass(Inventory inventory)
    {
        foreach (var key in oreNeeds.Keys)
        {
            if (!inventory.IsPlayerHasEnoughItem(key, oreNeeds[key]))
            {
                Debug.Log("NAHH");
                return;
            }
        }

        foreach (var key in oreNeeds.Keys)
        {
            inventory.RemoveItem(key, oreNeeds[key]);
        }
        cleared = true;
        GameManager.instance.OnCompleteCheckpoint();
    }

}