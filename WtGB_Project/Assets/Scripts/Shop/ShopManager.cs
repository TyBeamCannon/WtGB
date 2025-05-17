using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("UI Elements")]
    [SerializeField] GameObject content;
    [SerializeField] GameObject buyObjectPrefab;
    [SerializeField] GameObject sellButtonPrefab;

    [SerializeField] float buttonWidth;
    [SerializeField] float buttonHeight;

    float buyContentSize;


    [Header("Items")]
    [SerializeField] List<Item> shopItemsOg;

    [SerializeField] public List<Item> playerItems;
    [SerializeField] public List<Item> shopItems;

    private void Start()
    {
        buyContentSize = 50 + buttonHeight;
        if (instance == null)
            instance = this;

        ResetShop();
    }

    private void Update()
    {
        
    }

    public void ResetShop()
    {
        shopItems.Clear();
        shopItems = shopItemsOg;
    }

    public void GivePlayerItem(Item item)
    {
        if (playerItems.Count < 33)
        {
            playerItems.Add(item);
        }
    }

    public void SellItem(Item item)
    {
        
    }
}