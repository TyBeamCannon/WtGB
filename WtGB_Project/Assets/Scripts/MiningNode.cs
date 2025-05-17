using UnityEngine;

public class MiningNode : MonoBehaviour
{
    [Header("Ore Data")]
    [SerializeField] private int hitsToBreak = 3;
    [SerializeField] private GameObject oreDropPrefab;
    [SerializeField] private OreItem oreData;
    [SerializeField] private Vector3 dropOffset = Vector3.up;

    private int currentHits = 0;

    public void Mine()
    {
        currentHits++;

        if(currentHits >= hitsToBreak )
        {
            BreakNode();
        }
        else
        {
            //we can add like crumbles or something here
        }
    }

    private void BreakNode()
    {
        if(oreDropPrefab != null && oreData != null)
        {
            GameObject drop = Instantiate(oreDropPrefab, transform.position + dropOffset, Quaternion.identity);
            drop.GetComponent<OrePickup>().oreData = oreData;
        }
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
