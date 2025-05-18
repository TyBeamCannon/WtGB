using UnityEngine;

[CreateAssetMenu(fileName = "New Ore", menuName = "Ore")]
public class OreItem : ScriptableObject
{

    public string nameOfOre;
    public Sprite oreIcon;
    public int oreSellPrice;
    public string oreDescription;

}
