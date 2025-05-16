using UnityEngine;
using System.Collections;

// Can be used to tag an item as an equippable for future
public enum SlotTag { None };

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
   public Sprite sprite;
    public SlotTag itemTag;
}
