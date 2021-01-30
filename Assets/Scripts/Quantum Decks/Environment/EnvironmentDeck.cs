﻿using System;
using System.Collections.Generic;
using System.Linq;
using Quantum_Decks.Card_System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Environment
{
    public class EnvironmentDeck : MonoBehaviour
    {
        [ShowInInspector] private readonly List<EnvironmentCard> _cards = new List<EnvironmentCard>();

        public EnvironmentCardData DEBUG_CARD;

        public int Count => _cards.Count;

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

        public EnvironmentCard GetByPlayer(Networking.Player playerId)
        {
            switch (playerId)
            {
                case Networking.Player.Unset:
                    return null;
                case Networking.Player.One:
                    return _cards.FirstOrDefault();
                case Networking.Player.Two:
                    return _cards.LastOrDefault();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RemoveAllDefeated()
        {
            _cards.RemoveAll(d => d.Value == 0);
        }
    }
}