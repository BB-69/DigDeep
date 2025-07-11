using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider slider;

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void ChaheVolume()
    {
        AudioListener.volume = slider.value;
    }
    private void Load()
    {
        slider.value =PlayerPrefs.GetFloat("musicVolume");
    }
    private void save()
    {
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }
}