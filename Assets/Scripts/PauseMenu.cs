using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        Time.timeScale = pauseMenuUI.activeSelf ? 0 : 1; 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
}