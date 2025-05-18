using NUnit.Framework.Internal.Filters;
using UnityEngine;

[CreateAssetMenu(fileName = "newFish", menuName = "Fish")]
public class FishItem : ScriptableObject
{

    public string fishName;
    public Sprite icon;
    public int sellPrice;
    public string description;

}

