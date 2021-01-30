using Quantum_Decks.Card_System;
using UnityEngine;

namespace Shared.Scriptable_References
{
    [CreateAssetMenu(menuName = "Quantum/Collection/New Player Card Data Collection",
        fileName = "New [Player Card Data Collection]")]
    public class PlayerCardDataCollection : ScriptableCollection<PlayerCardData>{}
}