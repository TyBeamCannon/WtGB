using UnityEngine;

public class OrePickup : MonoBehaviour
{
    public Item ore;
    public OreItem oreData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(oreData != null)
        {
            GetComponent<SpriteRenderer>().sprite = oreData.oreIcon;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.InventoryItems.Add(ore);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
