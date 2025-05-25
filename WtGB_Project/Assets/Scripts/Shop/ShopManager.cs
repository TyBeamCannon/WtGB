using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("UI Elements")]
    [SerializeField] GameObject buyContent;
    [SerializeField] GameObject sellContent;
    [SerializeField] Text goldCount;
    [SerializeField] ShopSlot buyObjectPrefab;
    [SerializeField] ShopSlot sellButtonPrefab;

    [SerializeField] GameObject shopUI;

    [Header("Items")]
    [SerializeField] InventoryItem itemPrefab;

    [SerializeField] List<Item> shopItemsOg;

    [SerializeField] public List<Item> playerItems;
    [SerializeField] public List<Item> shopItems;

    bool shopUIOpen;

    private void Start()
    {
        goldCount.text = GameManager.instance.GoldCount.ToString();

        if (instance == null)
            instance = this;

        ResetShop();

        for (int i = 0; i < shopItems.Count; i++)
        {
            buyObjectPrefab.Initialize(shopItemsOg[i]);
            Instantiate(buyObjectPrefab, buyContent.transform);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shopUIOpen)
        {
            CloseShopUI();
        }
    }

    public void ResetShop()
    {
        shopItems.Clear();
        shopItems = new List<Item>(shopItemsOg);
    }

    public void GivePlayerItem(Item item)
    {
        if (playerItems.Count < 33)
        {
            playerItems.Add(item);
            sellButtonPrefab.Initialize(item);
            Instantiate(sellButtonPrefab, sellContent.transform);
            GameManager.instance.GoldCount -= item.buyPrice;
        }

        goldCount.text = GameManager.instance.GoldCount.ToString();
    }

    public void SellItem(Item item)
    {
        playerItems.Remove(item);
        shopItems.Add(item);
        GameManager.instance.GoldCount += item.sellPrice;

        goldCount.text = GameManager.instance.GoldCount.ToString();
    }

    public void OpenShopUI()
    {
        playerItems = new List<Item>(GameManager.instance.InventoryItems);

        shopUIOpen = true;
        shopUI.SetActive(true);

        goldCount.text = GameManager.instance.GoldCount.ToString();

        for (int i = 0; i < playerItems.Count; i++)
        {
            sellButtonPrefab.Initialize(playerItems[i]);
            Instantiate(sellButtonPrefab, sellContent.transform);
        }
    }

    public void CloseShopUI()
    {
        if (shopUIOpen)
        {
            shopUIOpen = false;

            shopUI.SetActive(false);


            GameManager.instance.InventoryItemSlots.Clear();
            GameManager.instance.data.PlayerItems.Clear();


            for (int k = 0; k < playerItems.Count; k++)
            {
                GameManager.instance.InventoryItems.Add(playerItems[k]);
                GameManager.instance.InventoryItemSlots.Add(k);
                Destroy(sellContent.transform.GetChild(k).gameObject);
            }
        }
    }
}