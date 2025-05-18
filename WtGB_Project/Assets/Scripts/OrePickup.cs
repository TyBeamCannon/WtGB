using UnityEngine;

public class OrePickup : MonoBehaviour
{

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
            Debug.Log($"Picked up: {oreData.nameOfOre}! Sells for {oreData.oreSellPrice} coins.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
