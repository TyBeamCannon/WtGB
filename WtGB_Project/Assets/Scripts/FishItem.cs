using NUnit.Framework.Internal.Filters;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Items/Fish")]
public class FishItem : ScriptableObject
{

    public string fishName;
    public Sprite icon;
    public int sellPrice;
    public string description;

}

