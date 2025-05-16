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
            UpdateVisual();
        }
    }

    public void Till()
    {
        if(!isTilled)
        {
            isTilled = true;
            spriteRenderer.sprite = tilledSprite;
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
            Debug.Log("Crop harvested: " + plantedSeed.cropName);
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
