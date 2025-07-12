using UnityEngine;

public class Quits : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("ออกละอีควาย");
        }
    }
}
