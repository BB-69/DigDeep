using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public static ResetButton Instance { get; private set; }
    public bool canPress = false; // Can the reset button be pressed?
    [SerializeField] float timeToTrigger =3f;
    [SerializeField] float currentHoldTime = 0f;
    [SerializeField] float waitTime = 10f; //wait time before the reset button can be used again
    [Header("UI")]
    [SerializeField] GameObject resetButtonUI;
    [SerializeField] Slider slider;

    void Awake()
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

    void OnEnable()
    {
        GameManager.OnLevelStart += SetCanReset;
        SetCanReset();
    }

    void OnDisable()
    {
        GameManager.OnLevelStart -= SetCanReset;
    }

    public void SetCanReset()
    {
        StartCoroutine(UpdateWaitTime());
    }

    void Update()
    {
        if(!canPress) return;
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentHoldTime = 0f;
            resetButtonUI.SetActive(true);
        }

        if (Input.GetKey(KeyCode.R))
        {
            currentHoldTime += Time.deltaTime;
            SetSlider();
            CheckIfReset();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            resetButtonUI.SetActive(false);
            currentHoldTime = 0f;
        }
    }

    IEnumerator UpdateWaitTime()
    {
        canPress = false;
        yield return new WaitForSeconds(waitTime);
        canPress = true;
    }

    void CheckIfReset()
    {
        if (currentHoldTime >= timeToTrigger)
        {
            currentHoldTime = 0f;
            resetButtonUI.SetActive(false);
            GameManager.instance.ResetLevel();
        }
    }

    void SetSlider()
    {
        slider.value = currentHoldTime / timeToTrigger;
    }
    
}