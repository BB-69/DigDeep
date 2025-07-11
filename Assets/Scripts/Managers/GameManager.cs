using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance{ get; private set; }
    [SerializeField] int checkpointPassed =0;
    public static UnityAction OnLevelStart; //call from checkpoint
    public int level = 0;
    public bool isStarted;
    public float timer { get; private set; } = 0;
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
        DontDestroyOnLoad(gameObject);  
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
            timer += Time.deltaTime;
        }
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
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
        isStarted = false;
        Debug.Log(timer);
        timer = 0;
        //do something before go to next level
        ChangeToNextLevel();
    }

    public void ChangeToNextLevel()
    {
        OnLevelStart?.Invoke();
        level++;
        if (level <= 3)
        {
            DungeonManager.instance.SetValue(); //default
        }
        else if (level <= 6)
        {
            DungeonManager.instance.SetValue(3f, 2f, .5f, 0);
        }
        else
        {
            DungeonManager.instance.SetValue(2f, 3f, 1f, .5f);
        }

        DungeonManager.instance.Regenerate();
        isStarted = true;
    }
}
