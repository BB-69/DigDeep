using UnityEngine;

public class Setting : MonoBehaviour
{

    public GameObject buttonToShow;



    public void Toggle()
    {
        buttonToShow.SetActive(!buttonToShow.activeSelf);                
    }
}
