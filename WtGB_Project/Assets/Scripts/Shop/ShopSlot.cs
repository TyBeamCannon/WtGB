using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] public Item item;

    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemSprite;
    [SerializeField] TMP_Text itemPrice;

    [SerializeField] Button shopSellButton;
    [SerializeField] Button shopBuyButton;

    private void Start()
    {
        itemName.text = item.itemName;
        itemSprite.sprite = item.sprite;
        if (shopBuyButton != null)
        {
            itemPrice.text = item.buyPrice.ToString();
            shopBuyButton.onClick.AddListener(delegate { BuyItem(); });
        }
        else if (shopSellButton != null)
        {
            itemPrice.text = item.sellPrice.ToString();
            shopSellButton.onClick.AddListener(delegate { SellItem(); });
        }
    }

    public void Initialize(Item item)
    {
        this.item = item;
    }

    public void BuyItem()
    {
        if (GameManager.instance.GoldCount >= item.buyPrice)
        {
            ShopManager.instance.GivePlayerItem(item);
        }
    }

    public void SellItem()
    {
        ShopManager.instance.SellItem(item);
        Destroy(gameObject);
    }
}
