using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour, IInteractable
{
    [Header("UI")]
    string DisplayText = "Supplies needed: ";
    [SerializeField] GameObject UIShowOreNeeds;
    [SerializeField] TextMeshProUGUI TextToShow;
    [SerializeField] RectTransform TextRect;
    [Header("Scripts")]
    public bool cleared = false;
    public bool isSpawnpoint = false;
    Dictionary<string, int> oreNeeds = new Dictionary<string, int>();

    void Start()
    {
        UIShowOreNeeds.SetActive(false);
    }

    public void Setup(int copper, int iron, int gold, int emerald)
    {
        oreNeeds["copper"] = copper;
        oreNeeds["iron"] = iron;
        oreNeeds["gold"] = gold;
        oreNeeds["emerald"] = emerald;

        SetDisplayText();
    }

    void SetDisplayText()
    {
        foreach (var key in oreNeeds.Keys)
        {
            if (oreNeeds[key] > 0)
            {
                DisplayText += $"\n{key}: {oreNeeds[key]}";
            }
        }
        TextToShow.text = DisplayText;
        float preferedTextHeight = TextToShow.preferredHeight;
        TextRect.sizeDelta = new Vector2(TextRect.sizeDelta.x, preferedTextHeight);

    }

    public void OnEnterInteractRange()
    {
        UIShowOreNeeds.SetActive(true);
    }

    public void OnExitInteractRange()
    {
        UIShowOreNeeds.SetActive(false);
    }

    public void Interact(PlayerManager player)
    {
        if (isSpawnpoint || cleared) return;
        CheckIfCanPass(player.inventory);
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