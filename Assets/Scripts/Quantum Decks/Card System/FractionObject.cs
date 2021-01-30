using UnityEngine;
using UnityEngine.UI;

namespace Quantum_Decks.Card_System
{
    public class FractionObject : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;

        public void UpdateFraction(Fraction fraction)
        {
            _background.color = fraction.Color;
            _icon.sprite = fraction.Sprite;
        }
    }
}