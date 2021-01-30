using System.Collections.Generic;
using Quantum_Decks.Card_System;
using UnityEngine;

namespace Quantum_Decks.Environment
{
    public class EnvironmentDeck : MonoBehaviour
    {
        private readonly List<EnvironmentCard> _cards = new List<EnvironmentCard>();
        
        public EnvironmentCardData DEBUG_CARD;

        private void Awake()
        {
            PopulateList();
        }

        public void PopulateList()
        {
            for (var i = 0; i < 30; i++)
            {
                _cards.Add(new EnvironmentCard(DEBUG_CARD));
            }
        }
    }
}
