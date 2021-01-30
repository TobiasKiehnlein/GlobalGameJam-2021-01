using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/New Fraction", fileName = "New Fraction Data [Fraction Data]")]
    public class Fraction : ScriptableObject
    {
        [Required]
        public string NameId;
        public Color Color;
        public Sprite Sprite;
    }
}