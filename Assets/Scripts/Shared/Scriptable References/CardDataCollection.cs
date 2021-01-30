using Quantum_Decks.Card_System;
using UnityEngine;

namespace Shared.Scriptable_References
{
    [CreateAssetMenu(menuName = "Quantum/Collection/New Card Data Collection",
        fileName = "New Card Data Collection [Card Data Collection]")]
    public class CardDataCollection : ScriptableCollection<CardData>
    {
    }
}