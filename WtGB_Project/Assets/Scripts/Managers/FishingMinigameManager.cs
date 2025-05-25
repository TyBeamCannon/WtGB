using UnityEngine;

public class FishingMinigameManager : MonoBehaviour
{
    public static FishingMinigameManager instance;

    [SerializeField] private GameObject fishDrop;
    [SerializeField] private FishItem fishCaught;
    [SerializeField] private Transform dropPoint;

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


        // Come back to start movement logic for the fish spawned
    }

    public void EndFishingMinigame(bool success)
    {
        fishingUI.SetActive(false);
        Time.timeScale = 1f;

        if(success)
        {
            if(fishDrop != null && fishCaught != null)
            {
                GameObject drop = Instantiate(fishDrop, dropPoint.position, Quaternion.identity);
                drop.GetComponent<FishPickup>().fishData = fishCaught;
            }

        }
        else
        {
        }

    }

}
