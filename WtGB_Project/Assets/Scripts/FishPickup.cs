using UnityEngine;

public class FishPickup : MonoBehaviour
{
    public FishItem fishData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(fishData != null)
        {
            GetComponent<SpriteRenderer>().sprite = fishData.icon;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Insert Inventory Logic here
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
