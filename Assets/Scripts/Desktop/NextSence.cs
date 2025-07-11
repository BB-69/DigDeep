using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSence : MonoBehaviour
{

    public CanvasGroup fadePanel;
    public float fadeSpeed = 1f;
    private bool fading = false;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || (Input.GetKeyDown(KeyCode.Return))) && !fading)
        {
            fading = true;
            StartCoroutine(FadeAndLoad());
        }
    }

    IEnumerator FadeAndLoad()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadePanel.alpha = alpha;
            yield return null;
        }

        SceneManager.LoadScene("Dungeon Test V2"); 
    }
}
