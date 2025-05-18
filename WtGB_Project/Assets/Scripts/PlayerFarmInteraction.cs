using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerFarmInteraction : MonoBehaviour
{

    private FarmTile currentTile;
    public SeedData seedToPlant;

    //Reference to the fatigue manager
    [SerializeField] private FatigueManager fatigueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       // Debug.Log("PlayerFarmInteraction Update running");

        if(currentTile == null)
            return;
        


        if (Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("Pressed E near Tile");

            if(!currentTile.IsTilled())
            {
                if(fatigueManager.UseStamina(5))
                {
                    currentTile.Till();
                }
                return;
            }

            if(!currentTile.IsPlanted())
            {
                if(fatigueManager.UseStamina(3))
                {
                    currentTile.Plant(seedToPlant);
                }
                return;
            }

            if(!currentTile.IsWatered() && !currentTile.IsFullyGrown())
            {
                if(fatigueManager.UseStamina(2))
                {
                    currentTile.Water();
                }
                return;
            }

            if(currentTile.IsFullyGrown())
            {
                if(fatigueManager.UseStamina(4))
                {
                    currentTile.Harvest();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FarmTile tile))
        {
            Debug.Log("Entered FarmTile: " + tile.name);
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