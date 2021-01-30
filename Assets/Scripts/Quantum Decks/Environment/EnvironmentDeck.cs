using System;
using System.Collections.Generic;
using System.Linq;
using Quantum_Decks.Card_System;
using Shared;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Environment
{
    public class EnvironmentDeck : MonoBehaviour
    {
        [SerializeField] private List<EnvironmentCardData> _allCardData;
        [SerializeField] private List<EnvironmentCardData> _allBossData;
        [ShowInInspector] private readonly List<EnvironmentCard> _cards = new List<EnvironmentCard>();
        [SerializeField] private EnvironmentDeckReference _environmentDeckReference;

        public int Count => _cards.Count;

        private void Awake()
        {
            PopulateList();
        }

        private void OnEnable()
        {
            _environmentDeckReference.Value = this;
        }

        private void OnDisable()
        {
            _environmentDeckReference.Reset();
        }

        public void PopulateList()
        {
            foreach (var cardData in _allCardData)
            {
                _cards.Add(new EnvironmentCard(cardData));
            }
            
            _cards.Shuffle();
        }

        public EnvironmentCard GetByPlayer(Networking.Player playerId)
        {
            if (!_cards.Any())
            {
                SpawnBoss();
            }
            
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

        public void SpawnBoss()
        {
            _allBossData.Shuffle();
            _cards.Add( new EnvironmentCard(_allBossData.First()));
            SpawnBossAnimation();
        }

        public void SpawnBossAnimation()
        {
            
        }

        public void RemoveAllDefeated()
        {
            _cards.RemoveAll(d => d.Value == 0);
        }
        
    }
}