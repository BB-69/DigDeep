using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool isStarted = false;
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isStarted) return;
        //Start level after player exit checkpoint
        if (collision.CompareTag("Player"))
        {
            isStarted = true;
            GameManager.OnLevelStart?.Invoke();
        }
    }
}