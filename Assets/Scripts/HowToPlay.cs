using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Text text;

    public GameObject howToPanel;

    void Start()
    {
        howToPanel.SetActive(false);
        text.text = "Press [H] to view how to play?";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            bool isActive = howToPanel.activeSelf;
            howToPanel.SetActive(!isActive);
        }
    }
}
