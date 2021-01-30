using System.Collections.Generic;
using System.Linq;
using Shared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class CardCollection : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private readonly List<PlayerCard> _cards = new List<PlayerCard>();
        public IEnumerable<PlayerCard> Cards => _cards.AsReadOnly();

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
    }
}