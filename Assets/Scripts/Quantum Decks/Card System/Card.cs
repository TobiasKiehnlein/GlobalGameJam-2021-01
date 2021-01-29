using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class Card
    {
        private CardData _data;

        public string NameId => _data.NameId;
        public string DescriptionId => _data.DescriptionId;
        public Fraction[] Fractions => _data.Fractions;
        public Sprite Sprite => _data.Sprite;
        
        public int Value;
        public int Duration;

        public Card(CardData data)
        {
            _data = data;
            Value = data.Value;
            Duration = data.Duration;
        }
    }
}