using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace CardStats
{
    public class CardBattleUIManager : MonoBehaviour
    {
        public PlayerDeck PlayerDeck;
        public Transform handPanel;
        public Button drawCardButton;
        public Button cardButtonPrefab;
        public TextMeshProUGUI turnStatusText;

        public Queue<Card> drawPile;
        [SerializeField] List<Card> hand = new List<Card>();
        int maxHandSize = 5;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            drawPile = new Queue<Card>(PlayerDeck.deck);
            drawCardButton.onClick.AddListener(DrawCard);
            turnStatusText.text = "Start Turn";
        }

        public void StartBattle()
        {
            if(PlayerDeck == null || handPanel == null || cardButtonPrefab == null)
            {
                Debug.LogError("Missing references in CardBattleUIManager");
                return;
            }
        }

        public void DrawCard()
        {
            if (hand.Count >= maxHandSize || drawPile.Count == 0)
                return;

            Card card = drawPile.Dequeue();
            hand.Add(card);
            RenderHand();
        }

        void RenderHand()
        {
            foreach (Transform child in handPanel)
                Destroy(child.gameObject);

            foreach(Card c in hand)
            {
                Card localCard = c;
                Button cardObj = Instantiate(cardButtonPrefab, handPanel);
                TextMeshProUGUI label = cardObj.GetComponentInChildren<TextMeshProUGUI>();
                if (label != null)
                    label.text = localCard.cardName;

                Button btn = cardObj.GetComponent<Button>();
                if(btn != null)
                {
                    btn.onClick.AddListener(() => OnCardButtonClick(localCard));
                }
            }
        }

        void OnCardButtonClick(Card card)
        {
            PlayCard(card);
        }

        public void PlayCard(Card card)
        {
            Debug.Log($"Played Card: {card.cardName}");

            hand.Remove(card);
            RenderHand();
        }
        
    }

}