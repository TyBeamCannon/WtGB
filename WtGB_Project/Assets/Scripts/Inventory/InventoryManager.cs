using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public static InventoryItem carriedItem;

    [SerializeField] public GameObject settingsScreen;

    [SerializeField] public InventorySlot[] inventorySlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    private void Awake()
    {
        instance = this;
        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if (_item == null)
        {
            int random = Random.Range(0, items.Length);
            _item = items[random];
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                break;
            }
        }
    }

    private void Update()
    {
        if (carriedItem == null)
            return;
        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null && item.activeSlot == null)
        {
            item.activeSlot.SetItem(carriedItem);
        }
        else if (carriedItem == null && item.activeSlot != null)
        {
            carriedItem = item;
            carriedItem.canvasGroup.blocksRaycasts = false;
            item.transform.SetParent(draggablesTransform);
        }
    }

    public void LoadIntoInventory(Item item, int slotNum = -1)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (slotNum >= 0)
            {
                if (slotNum == i)
                {
                    Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(item, inventorySlots[i]);
                    break;
                }
            }
            else if (slotNum == -1)
            {
                SpawnInventoryItem(item);
            }
        }
    }

    public List<Item> SaveItemFromInventory()
    {
        List<Item> invItems = new List<Item>();
        for (int i = 0;i < inventorySlots.Length;i++)
        {
            if (inventorySlots[i].myItem != null)
            {
                if (inventorySlots[i].myItem.myItem != null)
                {
                    invItems.Add(inventorySlots[i].myItem.myItem);
                }
            }
        }
        return invItems;
    }

    public List<int> SaveItemSlotFromInventory()
    {
        List<int> invItemSlot = new List<int>();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem != null)
            {
                invItemSlot.Add(i);
            }
        }
        return invItemSlot;
    }
}
