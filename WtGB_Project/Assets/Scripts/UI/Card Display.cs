using UnityEngine;
using UnityEngine.UI;
using CardStats;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Image cardImageUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(card != null && cardImageUI != null && card.cardImage != null)
        {
            cardImageUI.sprite = (Sprite)card.cardImage;
        }
        else
        {
            Debug.Log("CardDisplay: Missing References. Check Card, CardImageUI, or card.cardUI");
        }
    }
}
