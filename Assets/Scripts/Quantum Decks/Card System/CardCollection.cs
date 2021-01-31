using System.Collections.Generic;
using System.Linq;
using Shared;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class CardCollection : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private readonly List<PlayerCard> _cards = new List<PlayerCard>();
        public List<PlayerCard> Cards => _cards;
        
        [SerializeField] private CardCollectionReference _cardCollectionReference;

        private void OnEnable()
        {
            if (_cardCollectionReference)
            {
                _cardCollectionReference.Value = this;
            }
        }

        private void OnDisable()
        {
            if (_cardCollectionReference)
            {
                _cardCollectionReference.Reset();
            }
        }

        public void CreatAndAdd(PlayerCardData card)
        {
            Add(new PlayerCard(card));
        }

        public void Add(PlayerCard card)
        {
            _cards.Add(card);
        }

        public void Remove(PlayerCard card)
        {
            if (!_cards.Contains(card))
                return;

            _cards.Remove(card);
        }

        public void DrawTo(CardCollection target)
        {
            var card = _cards.FirstOrDefault();
            if (card == null)
                return;
            
            Transfer(card, target);
        }

        public void DrawTo(string cardId, CardCollection target)
        {
            var card = _cards.FirstOrDefault(c => c.NameId == cardId);
            if (card == null)
                return;
            
            Transfer(card, target);
        }
        
        public void Transfer(PlayerCard card, CardCollection target)
        {
            if (!_cards.Contains(card))
                return;

            Remove(card);
            target.Add(card);
        }

        public void Shuffle()
        {
            _cards.Shuffle();
        }

        public PlayerCard GetRandom()
        {
            Shuffle();
            return _cards.FirstOrDefault();
        }

        public static void QuantumShift(CardCollection target, CardCollection destiny)
        {
            var targetCards = target.Cards.ToList();
            var destinyCards = destiny.Cards.ToList();
            destinyCards.Clear();
            targetCards.Clear();
            targetCards.AddRange(destinyCards);
            destinyCards.AddRange(targetCards);
        }
    }
}