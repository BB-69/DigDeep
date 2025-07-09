using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
    public bool cleared = false;
    public bool isSpawnpoint = false;

    public void Interact()
    {
        if (isSpawnpoint) return;
        //condition check if can go
    }

    public void OnGoToNextLevel()
    {
        GameManager.instance.OnLevelCleared();
    }
}