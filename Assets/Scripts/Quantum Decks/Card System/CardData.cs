using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/New Card", fileName = "New Card Data [Card Data]")]
    public class CardData : ScriptableObject
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
        [MinValue(1)]
        public int Duration = 1;

        public Effect Effect;
    }
}
