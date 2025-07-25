using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public Camera MainCamera
    {
        get { return mainCamera; }
        set { mainCamera = value; }
    }
    public static GameManager instance { get; private set; }
    [SerializeField] int checkpointPassed = 0;
    public static UnityAction OnLevelStart; //call from checkpoint
    public int level = 0;
    public bool isStarted;
    public float totalTime { get; private set; } = 0f;
    public float timer { get; private set; } = 0;
    public float timeLimit { get; private set; } = 60f;
    public int totalXp { get; private set; } = 0;
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
    }
    void Start()
    {
        SoundManager.Instance.PlayMusic("bg");
        ChangeToNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
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
        PlayerManager.instance.bombThrower.AddBomb(CalculateBombToAdd());
        Debug.Log($"Complete {checkpointPassed} checkpoints");
        if (checkpointPassed == 4)
        {
            OnLevelCleared();
        }
    }

    int CalculateBombToAdd()
    {
        if (level < 3)
        {
            return 5;
        }
        else if (level < 5)
        {
            return 8;
        }
        else
        {
            return 10;
        }
    }

    public void OnLevelCleared()
    {
        //show ui
        totalTime += timer;
        isStarted = false;
        Debug.Log(timer);
        timer = 0;
        //do something before go to next level
        ChangeToNextLevel();
    }

    public void ResetLevel()
    {
        totalTime += timer;
        isStarted = false;
        timer = 0;
        ChangeToNextLevel(true);
    }

    public void ChangeToNextLevel(bool isReset = false)
    {
        checkpointPassed = 0;
        if(!isReset)
            level++;
        OnLevelStart?.Invoke();
        if (level < 3)
        {
            timeLimit = 240f;
            DungeonManager.instance.SetValue(); //default
        }
        else if (level < 5)
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
    }

    public void LoseGame()
    {
        totalTime += timer;
        isStarted = false;
        PlayerManager.instance.playerMovement.canMove = false;
        ResetButton.Instance.canPress = false;
        UIManager.Instance.ShowLoseUI();
        Debug.Log("You lose!");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Desktop");
    }
}
