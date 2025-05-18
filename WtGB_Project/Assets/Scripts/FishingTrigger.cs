using System.Collections;
using UnityEngine;

public class FishingTrigger : MonoBehaviour
{

    private bool playerInZone = false;
    private bool isFishing = false;

    [SerializeField] public float minBiteTime = 2f;
    [SerializeField] public float maxBiteTime = 5f;

    //Reference to the fatigue manager
    [SerializeField] private FatigueManager fatigueManager;    
    

    // Update is called once per frame
    void Update()
    {
        if(playerInZone && !isFishing && Input.GetKeyDown(KeyCode.E))
        {
            if (fatigueManager.UseStamina(4))
            {
                StartCoroutine(HandleFishing());
            }
        }
    }

    private IEnumerator HandleFishing()
    {
        isFishing = true;
        Debug.Log("Casting line...");

        float waitTime = Random.Range(minBiteTime, maxBiteTime);
        Debug.Log("Waiting for " + waitTime + " seconds");
        yield return new WaitForSeconds(waitTime);

        Debug.Log("Fish bite! Starting minigame...");
        // need to make the minigame manager maybe into a straight up minigame manager
        Debug.Log("Fish bite! Starting minigame...");
        FishingMinigameManager.instance.StartFishingMinigame();

        isFishing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SomethingEntered: " + other.name);

        if(other.CompareTag("Player"))
        {
            Debug.Log("Player entered fishing zone!");
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Left fishing zone!");
            playerInZone = false;
        }
    }

}

