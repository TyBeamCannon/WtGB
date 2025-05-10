using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingFunctions : MonoBehaviour
{
    [SerializeField] Toggle windowToggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    private void Start()
    {
        Default();
    }


    public void Windowed()
    {
#if UNITY_EDITOR
        Debug.Log("Windowed = " + windowToggle.isOn.ToString());
#else
        Screen.fullScreen = !windowToggle.isOn;
#endif
    }

    public void Resolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                {
                    Screen.SetResolution(2560, 1440, !windowToggle.isOn);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1920, 1080, !windowToggle.isOn);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(1366, 768, !windowToggle.isOn);
                    break;
                }
            case 3:
                {
                    Screen.SetResolution(1280, 800, !windowToggle.isOn);
                    break;
                }
        }
    }

    public void Master()
    {
        Sfx();
        Music();
    }

    public void Sfx()
    {
        
            foreach (AudioSource audSource in AudioSource.FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
            {
                if (audSource.tag == "SFX")
                {
                    audSource.volume = sfxSlider.value * masterSlider.value;
                }
            }
    }

    public void Music()
    {
        foreach (AudioSource audSource in AudioSource.FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (audSource.tag == "Music")
            {
                audSource.volume = musicSlider.value * masterSlider.value;
            }
        }
    }

    public void Default()
    {
        masterSlider.value = 1;
        sfxSlider.value = 1;
        musicSlider.value = 1;
        Master();
        windowToggle.isOn = false;
        Windowed();
        resolutionDropdown.value = 1;
        Resolution();
    }
}
