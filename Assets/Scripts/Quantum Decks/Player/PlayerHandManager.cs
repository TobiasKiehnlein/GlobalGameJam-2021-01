using Quantum_Decks.Card_System;
using UnityEngine;

namespace Quantum_Decks.Player
{
    [RequireComponent(typeof(CardCollection))]
    public class PlayerHandManager : MonoBehaviour
    {
        private CardCollection _hand;

        private void Awake()
        {
            _hand = GetComponent<CardCollection>();
        }
    }
}