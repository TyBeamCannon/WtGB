using UnityEngine;

public class FishPickup : MonoBehaviour
{
    public FishItem fishData;
    public Item fish;

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
            GameManager.instance.InventoryItems.Add(fish);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
