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

        float waitTime = Random.Range(minBiteTime, maxBiteTime);
        yield return new WaitForSeconds(waitTime);

        // need to make the minigame manager maybe into a straight up minigame manager
        FishingMinigameManager.instance.StartFishingMinigame();

        isFishing = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

}

