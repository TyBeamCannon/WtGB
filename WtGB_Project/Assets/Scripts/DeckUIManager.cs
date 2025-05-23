using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace CardStats
{
    public class DeckUIManager : MonoBehaviour
    {
        public PlayerDeck playerDeck;
        public Transform deckPanel;
        public Button cardButtonPrefab;
        public Button openDeckButton;
        public TextMeshProUGUI deckCountText;
        public GameObject deckMenuPanel;

        public Button[] cardSlotButtons;

        [SerializeField] int maxDeckSize;

        bool isDeckMenuOpen = false;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (openDeckButton != null)
                openDeckButton.onClick.AddListener(ToggleDeckMenu);

            if (deckMenuPanel != null)
                deckMenuPanel.SetActive(false);

            for(int i = 0; i < cardSlotButtons.Length; i++)
            {
                int index = i;
                cardSlotButtons[i].onClick.AddListener(() => OnCardSlotClicked(index));
            }
        }    

        void OnCardSlotClicked(int index)
        {
            Debug.Log($"Card Slot {index} clicked!");
        }
    

        public void ToggleDeckMenu()
        {
            isDeckMenuOpen = !isDeckMenuOpen;

            if (deckMenuPanel != null)
                deckMenuPanel.SetActive(isDeckMenuOpen);

            if (isDeckMenuOpen)
                RenderDeck();
        }
  

        public void RenderAllCards()
        {
           if(deckPanel == null || cardButtonPrefab == null || deckCountText == null || playerDeck == null)
            {
                Debug.LogError("DeckUIUManager: one or more references are not assigned in the inspector");
                return;
            }

            foreach (Transform child in deckPanel)
                Destroy(child.gameObject);

            for(int i = 0; i < playerDeck.deck.Count; i++)
            {
                var localCard = playerDeck.deck[i];
                Button btn = Instantiate(cardButtonPrefab, deckPanel);
                TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>();
                if(label != null)
                {
                    label.text = localCard.cardName;
                }

                Button button = btn.GetComponent<Button>();
                int index = i;
                if(button != null)
                {
                    button.onClick.AddListener(() => RemoveFromDeck(localCard));
                }
            }

            deckCountText.text = $"Deck: {playerDeck.deck.Count}/{maxDeckSize}";
        }

        public void RenderDeck()
        {
           if(deckPanel == null || cardButtonPrefab == null || deckCountText == null || playerDeck == null)
            {
                Debug.LogError("DeckUIUManager: one or more references are not assigned in the inspector");
                return;
            }
        }

        public void AddToDeck(Card card)
        {
            if (playerDeck.deck.Count >= maxDeckSize || playerDeck.deck.Contains(card)) return;          
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

        public void ShuffleDeck()
        {
            for(int i = 0; i < playerDeck.deck.Count; i++)
            {
                Card temp = playerDeck.deck[i];
                int randomIndex = Random.Range(i, playerDeck.deck.Count);
                playerDeck.deck[i] = playerDeck.deck[randomIndex];
                playerDeck.deck[randomIndex] = temp;
            }
            RenderDeck();
        }

        //public void SaveLineUp()
        //{
        //    playerDeck.SetLineup(new List<Card>(playerDeck.deck));
        //    Debug.Log("Deck lineup was saved");
        //}

    }
}