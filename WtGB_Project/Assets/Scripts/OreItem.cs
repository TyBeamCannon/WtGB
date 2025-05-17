using UnityEngine;

[CreateAssetMenu(fileName = "New Ore", menuName = "Ore")]
public class OreItem : ScriptableObject
{

    public string oreName;
    public Sprite icon;
    public int sellPrice;
    public string description;

}
