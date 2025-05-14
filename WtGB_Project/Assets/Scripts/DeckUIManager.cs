using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace CardStats
{
    public class DeckUIManager : MonoBehaviour
    {
        public PlayerDeck playerDeck;

        public Transform allCardsPanel;
        public Transform deckPanel;
        public GameObject cardButtonPrefab;
        public TextMeshProUGUI deckCountText;

        [SerializeField] int maxDeckSize;




        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            RenderAllCards();
            RenderDeck();
        }

        public void RenderAllCards()
        {
            foreach (Transform child in allCardsPanel)
                Destroy(child.gameObject);

            foreach (var card in playerDeck.allCards)
            {
                var localCard = card;
                GameObject btn = Instantiate(cardButtonPrefab, allCardsPanel);
                TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (label != null)
                    label.text = localCard.cardName;

                Button button = btn.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(() => RemoveFromDeck(localCard));

            }
        }

        public void RenderDeck()
        {
            foreach (Transform child in deckPanel)
                Destroy(child.gameObject);

            foreach (var card in playerDeck.allCards)
            {
                var localCard = card;
                GameObject btn = Instantiate(cardButtonPrefab, allCardsPanel);
                TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (label != null)
                    label.text = localCard.cardName;

                Button button = btn.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(() => RemoveFromDeck(localCard));


            }

            deckCountText.text = $"Deck: {playerDeck.deck.Count}/{maxDeckSize}";
        }

        public void AddToDeck(Card card)
        {
            if (playerDeck.deck.Count >= maxDeckSize) return;
            if (!playerDeck.deck.Contains(card))
            {
                playerDeck.deck.Add(card);
                RenderDeck();
            }
        }

        public void RemoveFromDeck(Card card)
        {
            if (playerDeck.deck.Contains(card))
            {
                playerDeck.deck.Remove(card);
                RenderDeck();
            }
        }

        public void SaveLineUp()
        {
            playerDeck.SetLineup(new List<Card>(playerDeck.deck));
            Debug.Log("Deck lineup was saved");
        }

    }
}