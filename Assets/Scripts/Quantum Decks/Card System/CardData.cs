using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public abstract class CardData : ScriptableObject
    {
        [Required]
        public string NameId;
        [Required]
        public Fraction[] Fractions;
        [Required]
        public string DescriptionId;
        [Required]
        public int Value;
        public Sprite Sprite;
    }
}
