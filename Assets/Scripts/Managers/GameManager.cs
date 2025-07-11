using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] int checkpointPassed = 0;
    public static UnityAction OnLevelStart; //call from checkpoint
    public int level = 0;
    public bool isStarted;
    public float timer { get; private set; } = 0;
    float timeLimit = 60f;
    public int totalXp { get; private set; } = 0;
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
    }
    void Start()
    {
        ChangeToNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                LoseGame();
            }
        }
    }

    public void AddXp(int xp)
    {
        totalXp += xp;
        Debug.Log($"Total XP: {totalXp}");
    }

    public void OnCompleteCheckpoint()
    {
        checkpointPassed++;
        UIManager.Instance.AddCheckpointText(checkpointPassed);
        Debug.Log($"Complete {checkpointPassed} checkpoints");
        if (checkpointPassed == 4)
        {
            OnLevelCleared();
        }
    }

    public void OnLevelCleared()
    {
        //show ui
        isStarted = false;
        Debug.Log(timer);
        timer = 0;
        //do something before go to next level
        ChangeToNextLevel();
    }

    public void ChangeToNextLevel()
    {
        checkpointPassed = 0;
        OnLevelStart?.Invoke();
        level++;
        if (level <= 3)
        {
            timeLimit = 240f;
            DungeonManager.instance.SetValue(); //default
        }
        else if (level <= 6)
        {
            timeLimit = 300f;
            DungeonManager.instance.SetValue(3f, 2f, .5f, 0);
        }
        else
        {
            timeLimit = 400f;
            DungeonManager.instance.SetValue(2f, 3f, 1f, .5f);
        }

        DungeonManager.instance.Regenerate();
        isStarted = true;
        timer = timeLimit;
    }

    public void LoseGame()
    {
        isStarted = false;
        PlayerManager.instance.playerMovement.canMove = false;
        UIManager.Instance.ShowLoseUI();
        Debug.Log("You lose!");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Desktop");
    }
}
