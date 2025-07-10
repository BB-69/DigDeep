using UnityEngine;
using UnityEngine.UI;

public class TextDesktop : MonoBehaviour
{
    public Text text;

    public CanvasGroup canvasGroup;

    public float fadeSpeed = 1.5f;

    private bool fadingOut = true;

    void Start()
    {
        text.text = "--- Press Enter ---";
    }
    void Update()
    {
        if (fadingOut)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                fadingOut = false;
            }
        }
        else
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
                fadingOut = true;
            }
        }
    }
    

}
