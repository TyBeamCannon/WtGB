using UnityEngine;

public class FishingMinigameManager : MonoBehaviour
{
    public static FishingMinigameManager instance;

    [Header("UI Elements")]
    public GameObject fishingUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void StartFishingMinigame()
    {
        Time.timeScale = 0f;
        fishingUI.SetActive(true);
        Debug.Log("Fishing minigame started!");


        // Come back to start movement logic for the fish spawned
    }

    public void EndFishingMinigame(bool success)
    {
        fishingUI.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Fishing minigame ended!");

        if(success)
        {
            Debug.Log("You Caught fish!");
        }
        else
        {
            Debug.Log("You lost fish!");
        }

    }

}
