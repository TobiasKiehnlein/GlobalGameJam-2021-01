using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public abstract class Card
    {
        protected CardData _data;

        [BoxGroup, ShowInInspector, PropertyOrder(0)]
        public string NameId => _data.NameId;
        public string DescriptionId => _data.DescriptionId;
        public Fraction[] Fractions => _data.Fractions;
        public Sprite Sprite => _data.Sprite;
        
        [BoxGroup, PropertyOrder(1)]
        public int Value;

        protected Card(CardData data)
        {
            _data = data;
            Value = data.Value;
        }
    }
}