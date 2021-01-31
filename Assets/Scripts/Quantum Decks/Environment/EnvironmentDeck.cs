using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Quantum_Decks.Card_System;
using Shared;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using NetworkManager = Networking.NetworkManager;

namespace Quantum_Decks.Environment
{
    public class EnvironmentDeck : NetworkBehaviour
    {
        [SerializeField] private List<EnvironmentCardData> _allCardData;
        [SerializeField] private List<EnvironmentCardData> _allBossData;
        [Sirenix.OdinInspector.ShowInInspector] private List<EnvironmentCard> _cards = new List<EnvironmentCard>();
        [SerializeField] private EnvironmentDeckReference _environmentDeckReference;

        public int Count => _cards.Count;

        private void Awake()
        {
            PopulateList();
        }

        private void OnEnable()
        {
            _environmentDeckReference.Value = this;
            NetworkManager.OnEnvironmentChanged.AddListener(UpdateList);
        }

        private void OnDisable()
        {
            _environmentDeckReference.Reset();
            NetworkManager.OnEnvironmentChanged.RemoveListener(UpdateList);
        }

        [Command]
        public void PopulateList()
        {
            if (!isServer)
            {
                return;
            }
            Debug.Log("EVN: Populate List");
            
            foreach (var cardData in _allCardData)
            {
                _cards.Add(new EnvironmentCard(cardData));
            }
            
            _cards.Shuffle();
            
            NetworkManager.LocalPlayer.ChangeEnvironment(_cards.Select(c => c.NameId).ToArray());
        }

        public void UpdateList(string[] cards)
        {
            _cards = cards.Select(c => _cards.FirstOrDefault(a => a.NameId == c)).ToList();
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

        [Button(ButtonSizes.Medium)]
        public void Transmute(Networking.Player playerId)
        {
            if (_cards.Count <= 1)
            {
                return;
            }

            EnvironmentCard card;
            switch (playerId)
            {
                case Networking.Player.Unset:
                    break;
                case Networking.Player.One:
                    card = _cards.Last();
                    _cards.Remove(card);
                    _cards.Shuffle();
                    _cards.Add(card);
                    break;
                case Networking.Player.Two:
                    card = _cards.First();
                    _cards.Remove(card);
                    _cards.Shuffle();
                    _cards.Insert(0, card);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}