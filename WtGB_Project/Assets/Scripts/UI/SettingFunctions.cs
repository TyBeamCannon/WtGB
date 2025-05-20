using TMPro;
using Unity.VisualScripting;
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
        if (GameManager.instance == null || GameManager.instance.data == null)
        {
            Debug.LogWarning("GameManager or GameManager.data is missing!");
            return;
        }

        if (!GameManager.instance.data.SettingsInitialized)
        {
            Default();
            GameManager.instance.data.SettingsInitialized = true;
        }
        else
        {
            windowToggle.isOn = GameManager.instance.WindowedMode;
            resolutionDropdown.value = GameManager.instance.ResIndex;
            masterSlider.value = GameManager.instance.MasterValue;
            sfxSlider.value = GameManager.instance.SFXValue;
            musicSlider.value = GameManager.instance.MusicValue;
            Master();
            Windowed();
            Resolution();
        }
    }


    public void Windowed()
    {
#if UNITY_EDITOR
        Debug.Log("Windowed = " + windowToggle.isOn.ToString());
#else
    Screen.fullScreen = !windowToggle.isOn;
#endif

        if (GameManager.instance != null && GameManager.instance.data != null)
        {
            GameManager.instance.data.WindowedMode = !windowToggle.isOn;
        }
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
        GameManager.instance.ResIndex = resolutionDropdown.value;
    }

    public void Master()
    {
        Sfx();
        Music();
        GameManager.instance.MasterValue = masterSlider.value;
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
            GameManager.instance.SFXValue = sfxSlider.value;
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
        GameManager.instance.MusicValue = musicSlider.value;
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
