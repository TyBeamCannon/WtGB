using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Settings()
    {
        MainMenuManager.instance.MainMenuActive = false;

        MainMenuManager.instance.SettingsActive = true;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Credits()
    {
        MainMenuManager.instance.MainMenuActive = false;

        MainMenuManager.instance.CreditsActive = true;
    }

    public void ContinueDialogue()
    {
        DialogueManager.instance.DisplayNextDialogueLine();
    }
}
