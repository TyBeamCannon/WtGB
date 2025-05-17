using UnityEngine;

public class MiningNode : MonoBehaviour
{
    [Header("Ore Data")]
    [SerializeField] private int hitsToBreak = 3;
    [SerializeField] private GameObject oreDropPrefab;
    // Gave me an error because OreItem script was not pushed so it could not find the reference if you could please push that with next commit
    // [SerializeField] private OreItem oreData;
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
        if(oreDropPrefab != null/* && oreData != null*/)
        {
            GameObject drop = Instantiate(oreDropPrefab, transform.position + dropOffset, Quaternion.identity);
            // Reason why it's commented at top of script
            //drop.GetComponent<OrePickup>().oreData = oreData;
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
