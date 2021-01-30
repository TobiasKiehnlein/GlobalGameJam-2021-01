using Quantum_Decks.Card_System;
using UnityEngine;

namespace Shared.Scriptable_References
{
    [CreateAssetMenu(menuName = "Quantum/References/Card Collection", fileName = "new [Card Collection Reference")]
    public class CardCollectionReference : ScriptableReference<CardCollection>
    {
    }
}