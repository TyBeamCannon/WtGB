using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("UI Elements")]
    [SerializeField] GameObject buyContent;
    [SerializeField] GameObject sellContent;
    [SerializeField] ShopSlot buyObjectPrefab;
    [SerializeField] ShopSlot sellButtonPrefab;

    [Header("Items")]
    [SerializeField] List<Item> shopItemsOg;

    [SerializeField] public List<Item> playerItems;
    [SerializeField] public List<Item> shopItems;

    private void Start()
    {
        if (instance == null)
            instance = this;

        ResetShop();

        for (int i = 0; i < shopItemsOg.Count; i++)
        {
            buyObjectPrefab.Initialize(shopItemsOg[i]);
            Instantiate(buyObjectPrefab, buyContent.transform);
        }
    }

    private void Update()
    {
        for (int i = 0; i < playerItems.Count; i++)
        {
                    
        }
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
            sellButtonPrefab.Initialize(item);
            Instantiate(sellButtonPrefab, sellContent.transform);
        }
    }

    public void SellItem(Item item)
    {
        playerItems.Remove(item);
        shopItems.Add(item);
    }
}