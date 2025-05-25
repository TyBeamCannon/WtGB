using UnityEngine;

public class FarmTile : MonoBehaviour
{
    private bool isTilled = false;
    private bool isWatered = false;
    private bool isPlanted = false;

    public SeedData plantedSeed;
    private int growthStage = 0;
    private float growthTimer = 0f;

    public SpriteRenderer spriteRenderer;
    public Sprite untilledSprite;
    public Sprite tilledSprite;
    public Sprite[] growthStageSprites;

    public float growSpeedMult = 1f;

    [Header("Harvest Drop")]
    public GameObject harvestDropPrefab;
    public Vector3 dropOffset = new Vector3(0f, 1f, 0f);  


    private void Start()
    {
       
    }

    private void Update()
    {
        if(isPlanted && isWatered && plantedSeed != null)
        {
            growthTimer += Time.deltaTime * growSpeedMult;

            if(growthTimer >= plantedSeed.growthTimePerStage)
            {
                growthStage++;
                growthTimer = 0f;
                isWatered = false;

                if(growthStage >= plantedSeed.totalGrowthStages)
                {
                    growthStage = plantedSeed.totalGrowthStages;
                }
                UpdateVisual();
            }
        }
    }

    public void Plant(SeedData seed)
    {
        if(isTilled && !isPlanted)
        {
            plantedSeed = seed;
            isPlanted = true;
            growthStage = 0;
            growthTimer = 0f;

            harvestDropPrefab = seed.harvetDropPrefab;

            UpdateVisual();
        }
    }

    public void Till()
    {
        if(!isTilled)
        {
            isTilled = true;
            UpdateVisual();
        }
    }

    public void Water()
    {
        if (isPlanted)
        {
            isWatered = true;
        }
    }


    public void Harvest()
    {
        if(isPlanted && growthStage >= plantedSeed.totalGrowthStages)
        {

            if(harvestDropPrefab != null )
            {
                GameObject drop = Instantiate(harvestDropPrefab, transform.position + dropOffset, Quaternion.identity);
                Rigidbody rb = drop.GetComponent<Rigidbody>();
                if(rb != null )
                {
                    Vector3 launchDirection = Vector3.up * 4f + Vector3.right * Random.Range(-1f, 1f);
                    rb.AddForce(launchDirection, ForceMode.Impulse);
                }

            }

            ResetTile();
        }
    }

    public void ResetTile()
    {
        isTilled = true;
        isWatered = false;
        isPlanted = false;
        plantedSeed = null;
        growthStage = 0;
        growthTimer = 0f;
        spriteRenderer.sprite = tilledSprite;
    }

    private void UpdateVisual()
    {
        if (isTilled && !isPlanted)
        {
            spriteRenderer.sprite = tilledSprite;
            return;
        }

        if(isPlanted && growthStageSprites.Length > 0)
        {
            int index = Mathf.Clamp(growthStage, 0 , growthStageSprites.Length - 1);
            spriteRenderer.sprite = growthStageSprites[index];
        }
    }


    // Adding the getterssss and maybe setters 

    public bool IsTilled()
    {
        return isTilled;
    }

    public bool IsPlanted()
    {
        return isPlanted;
    }

    public bool IsWatered()
    {
        return isWatered;   
    }

    public bool IsFullyGrown()
    {
        return growthStage >= plantedSeed.totalGrowthStages;
    }

}
