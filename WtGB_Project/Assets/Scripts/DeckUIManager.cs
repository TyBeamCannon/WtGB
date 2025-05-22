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
        public Transform DeckPanel;
        public GameObject cardButtonPrefab;
        public TextMeshProUGUI deckCountText;
        public GameObject deckMenuPanel;
        public Button toggleDeckMenu;
        public Button openDeckButton;

        [SerializeField] int maxDeckSize;

        bool isDeckMenuOpen = false;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            toggleDeckMenu.onClick.AddListener(ToggleDeckMenu);
            openDeckButton.onClick.AddListener(OpenDeck);
            deckMenuPanel.SetActive(false);
            RenderAllCards();
            RenderDeck();
        }    
    

        public void ToggleDeckMenu()
        {
            isDeckMenuOpen = !isDeckMenuOpen;
            deckMenuPanel.SetActive(isDeckMenuOpen);

            if (isDeckMenuOpen)
            {
                RenderAllCards();
                RenderDeck();
            }
        }

        public void OpenDeck()
        {
            Debug.Log("OpenDeck is triggered");

            if (!DeckPanel.gameObject.activeSelf)
            {
                DeckPanel.gameObject.SetActive(true);
                RenderDeck();
            }
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
            foreach (Transform child in DeckPanel)
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