using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class CardCollection : MonoBehaviour
    {
        private readonly List<Card> _cards = new List<Card>();
        public IEnumerable<Card> Cards => _cards.AsReadOnly();

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        public void Remove(Card card)
        {
            if (!_cards.Contains(card))
                return;

            _cards.Remove(card);
        }

        public void Transfer(Card card, CardCollection target)
        {
            if (!_cards.Contains(card))
                return;

            _cards.Remove(card);
            target.Add(card);
        }

        public void Shuffle()
        {
            _cards.Shuffle();
        }
    }
}