using UnityEngine;


[CreateAssetMenu(menuName = "Seed Data")]
public class SeedData : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string cropName;
    public float growthTimePerStage = 2f;
    public int totalGrowthStages = 3;
    public GameObject harvetDropPrefab = null;
}
