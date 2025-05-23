using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{

    [SerializeField] SettingFunctions settings;

    public void Play()
    {
        settings.ButtonClick();

        SceneManager.LoadScene("MainGame");
        GameManager.instance.inMainMenu = false;
    }

    public void Settings()
    {
        settings.ButtonClick();

        MainMenuManager.instance.MainMenuActive = false;

        MainMenuManager.instance.SettingsActive = true;
    }

    public void Quit()
    {
        settings.ButtonClick();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Credits()
    {
        settings.ButtonClick();

        MainMenuManager.instance.MainMenuActive = false;

        MainMenuManager.instance.CreditsActive = true;
    }

    public void ContinueDialogue()
    {
        DialogueManager.instance.DisplayNextDialogueLine();
    }

    public void SwapInvSettings()
    {
        InventoryManager.instance.settingsScreen.SetActive(!InventoryManager.instance.settingsScreen.activeSelf);
    }

    public void ResRight()
    {
        GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().value--;
        GameObject.FindGameObjectWithTag("ResText").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().options[GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().value].text;
        settings.ButtonClick();

    }

    public void ResLeft()
    {
        GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().value++;
        GameObject.FindGameObjectWithTag("ResText").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().options[GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().value].text;
        settings.ButtonClick();
    }
}
