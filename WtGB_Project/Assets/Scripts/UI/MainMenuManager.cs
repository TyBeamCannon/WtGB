using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject credits;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (SettingsActive || CreditsActive)
            {
                if (SettingsActive)
                {
                    SettingsActive = false;
                    MainMenuActive = true;
                }
                else
                {
                    CreditsActive = false;
                    MainMenuActive = true;
                }
            }
        }
    }

    public bool MainMenuActive {  get { return mainMenu.activeSelf; } set { mainMenu.SetActive(value); } }
    public bool SettingsActive { get { return settingsScreen.activeSelf; } set { settingsScreen.SetActive(value); } }
    public bool CreditsActive { get { return credits.activeSelf; } set {  credits.SetActive(value); } }
}
