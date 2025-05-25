using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class SettingFunctions : MonoBehaviour
{
    [Header("SFX Audio Source")]
    [SerializeField] AudioSource sfx;

    [Header("Audio Clips")]
    [SerializeField] AudioClip resetDefault;
    [SerializeField] AudioClip checkboxOn;
    [SerializeField] AudioClip checkboxOff;
    [SerializeField] AudioClip sliderSliding;
    [SerializeField] AudioClip sliderSelect;
    [SerializeField] AudioClip sliderDeselect;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip buttonHover;

    [Header("Settings Adjusters")]
    [SerializeField] Toggle windowToggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] GameObject defaultButton;

    bool defaultBroke;

    private void Start()
    {
        if (sfx == null)
            sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        if (GameManager.instance == null || GameManager.instance.data == null)
        {
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
        if (!defaultBroke)
        {
            BreakDefaultButton();
        }
#if UNITY_EDITOR
#else
    Screen.fullScreen = !windowToggle.isOn;
#endif
        if (!windowToggle.isOn)
        {
            sfx.PlayOneShot(checkboxOff);
        }
        else
        {
            sfx.PlayOneShot(checkboxOn);
        }

        if (windowToggle != null && GameManager.instance != null)
        {
            GameManager.instance.WindowedMode = windowToggle.isOn;
        }
    }

    public void Resolution()
    {
        if (!defaultBroke)
        {
            BreakDefaultButton();
        }
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
        if (!defaultBroke)
        {
            BreakDefaultButton();
        }
        if (!sfx.isPlaying)
        {
            sfx.PlayOneShot(sliderSliding);
        }
        foreach (AudioSource audSource in AudioSource.FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (audSource.tag == "SFX")
            {
                audSource.volume = sfxSlider.value * masterSlider.value;
            }
        }
        foreach (AudioSource audSource in AudioSource.FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (audSource.tag == "Music")
            {
                audSource.volume = musicSlider.value * masterSlider.value;
            }
        }
        GameManager.instance.MasterValue = masterSlider.value;
        GameManager.instance.SFXValue = sfxSlider.value;
        GameManager.instance.MusicValue = musicSlider.value;
    }

    public void Sfx()
    {
        if (!defaultBroke)
        {
            BreakDefaultButton();
        }
        if (!sfx.isPlaying)
        {
            sfx.PlayOneShot(sliderSliding);
        }
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
        if (!defaultBroke)
        {
            BreakDefaultButton();
        }
        if (!sfx.isPlaying)
        {
            sfx.PlayOneShot(sliderSliding);
        }
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
        if (defaultBroke)
        {
            masterSlider.value = 1;
            sfxSlider.value = 1;
            musicSlider.value = 1;
            Master();
            windowToggle.isOn = false;
            Windowed();
            resolutionDropdown.value = 2;
            Resolution();
            FixDefaultButton();
        }
    }

    void FixDefaultButton()
    {
        if (defaultBroke)
        {
            defaultBroke = false;
            defaultButton.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            sfx.PlayOneShot(resetDefault);
        }
    }

    void BreakDefaultButton()
    {
        if (!defaultBroke)
        {
            defaultBroke = true;
            defaultButton.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, -15);
        }
    }

    public void SliderSelect()
    {
        sfx.PlayOneShot(sliderSelect);
    }

    public void SliderDeselect()
    {
        sfx.PlayOneShot(sliderDeselect);
    }

    public void ButtonClick()
    {
        sfx.PlayOneShot(buttonClick);
    }

    public void ButtonHover()
    {
        sfx.PlayOneShot(buttonHover);
    }
}
