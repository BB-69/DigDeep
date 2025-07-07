using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool visited = false;
    public bool isSpawnpoint = false;
    bool isStarted = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visited = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isStarted || !isSpawnpoint) return;
        //Start level after player exit checkpoint
        if (collision.CompareTag("Player"))
        {
            isStarted = true;
            GameManager.OnLevelStart?.Invoke();
        }
    }

    public void OnClickGoNextLevel()
    {
        if (isSpawnpoint) return;
        GameManager.instance.Level += 1;
    }
}