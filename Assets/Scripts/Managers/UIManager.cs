using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject loseUI;
    [SerializeField] TextMeshProUGUI loseDetailText;
    public GameObject PressToInteractUI;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI checkpointText;
    public TextMeshProUGUI bombCountText;

    public GameObject[] itemUI;
    public TextMeshProUGUI[] itemText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PressToInteractUI.SetActive(false);
    }

    void OnEnable()
    {
        GameManager.OnLevelStart += ResetUI;
    }

    void OnDisable()
    {
        GameManager.OnLevelStart -= ResetUI;
    }

    private void Update()
    {
        timerText.text = "Time: " + GameManager.instance.timer.ToString();
    }

    public void ResetUI()
    {
        levelText.text = "Level: " + GameManager.instance.level.ToString();
        timerText.text = "Time: 0";
        AddCheckpointText(0);
        PressToInteractUI.SetActive(false);
    }

    public void UpdateBombCount(int count)
    {
        bombCountText.text = "x"+count.ToString();
    }

    public void AddCheckpointText(int checkpointCleared)
    {
        checkpointText.text = $"Checkpoints: {checkpointCleared}/4";
    }

    public void UpdateInventoryUI(Dictionary<string, int> inventory)
    {
        for (int i = 0; i < itemUI.Length; i++)
        {
            string key = itemUI[i].name;
            int value = inventory.ContainsKey(key) ? inventory[key] : 0;

            itemUI[i].SetActive(value > 0);
            itemText[i].text = "x"+value.ToString();
        }
    }

    public void ShowLoseUI()
    {
        GameManager.instance.isStarted = false;
        loseUI.SetActive(true);
        loseDetailText.text = $"level passed: {GameManager.instance.level}\nxp gained: {GameManager.instance.totalXp}";
        Debug.Log("You lose");
    }

    public void ShowPressToInteractUI() => PressToInteractUI.SetActive(true);
    public void HidePressToInteractUI() => PressToInteractUI.SetActive(false);

}