using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Text text;

    public GameObject howToPanel;

    void Start()
    {
        howToPanel.SetActive(false);
        text.text = "Press [ESC] to view how to play?";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = howToPanel.activeSelf;
            howToPanel.SetActive(!isActive);
        }
    }
}
