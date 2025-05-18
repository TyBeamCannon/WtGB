using UnityEngine;

public class CropPickup : MonoBehaviour
{
    public SeedData seedData;
    public Item crop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (seedData != null)
        {
            GetComponent<SpriteRenderer>().sprite = seedData.cropSprite;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.InventoryItems.Add(crop);
            GameManager.instance.InventoryItemSlots.Add(GameManager.instance.InventoryItemSlots.Count);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
