using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerFarmInteraction : MonoBehaviour
{

    private FarmTile currentTile;
    public SeedData seedToPlant;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTile == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!currentTile.IsTilled())
            {
                currentTile.Till();
                return;
            }

            if(!currentTile.IsPlanted())
            {
                currentTile.Plant(seedToPlant);
                return;
            }

            if(!currentTile.IsWatered() && !currentTile.IsFullyGrown())
            {
                currentTile.Water();
                return;
            }

            if(currentTile.IsFullyGrown())
            {
                currentTile.Harvest();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FarmTile tile))
        {
            currentTile = tile;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out FarmTile tile) && tile == currentTile)
        {
            currentTile = null;
        }
    }

}