using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance{ get; private set; }
    public int Level;
    public static UnityAction OnLevelStart; //call from checkpoint
    [SerializeField] float timeLimit;
    [SerializeField] float currentTime = 0;
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this.gameObject); }
        DontDestroyOnLoad(gameObject);  
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        OnLevelStart += OnStartLevel;
    }

    void OnDisable()
    {
        OnLevelStart -= OnStartLevel;
    }

    public void OnStartLevel()
    {
        InvokeRepeating("Countdown", 1f, 1f);
    }

    void Countdown()
    {
        currentTime += 1;
        if (currentTime >= timeLimit)
        {
            //decrease hp
        }
    }
}
