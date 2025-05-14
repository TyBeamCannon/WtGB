
using UnityEngine;
using System.Collections.Generic;


namespace CardStats
{
    public class PlayerDeck : MonoBehaviour
    {
        public List<Card> allCards = new();
        public List<Card> deck = new();
        public List<Card> hand = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void AddCard(Card card)
        {
            if (!allCards.Contains(card))
                allCards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            if (allCards.Contains(card))
                allCards.Remove(card);

            if (deck.Contains(card))
                allCards.Add(card);
        }

        public void SetLineup(List<Card> newDeck)
        {
            deck = new List<Card>(newDeck);
        }

        public void ResetDeckForBattle()
        {
            hand.Clear();
        }

        public Card DrawCard()
        {
            if (deck.Count == 0) return null;

            int index = Random.Range(0, deck.Count);
            Card drawn = deck[index];
            hand.Add(drawn);
            return drawn;
        }

        public void PlayCard(Card card)
        {
            if (!hand.Contains(card)) return;

            var executor = gameObject.AddComponent<CardExecutor>();
            executor.card = card;
            executor.execute();
            hand.Remove(card);
        }
    }

}