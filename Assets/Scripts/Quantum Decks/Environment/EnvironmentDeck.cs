using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Networking;
using Quantum_Decks.Card_System;
using Shared;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Quantum_Decks.Environment
{
    public class EnvironmentDeck : MonoBehaviour
    {
        [SerializeField] private List<EnvironmentCardData> _allCardData;
        [SerializeField] private List<EnvironmentCardData> _allBossData;

        [Sirenix.OdinInspector.ShowInInspector]
        private List<EnvironmentCard> _cards = new List<EnvironmentCard>();

        [SerializeField] private EnvironmentDeckReference _environmentDeckReference;
        [SerializeField] private NetworkSettingReference _networkSettingReference;

        [SerializeField] private int NumberOfCards = 17;

        public int Count => _cards.Count;

        private void Awake()
        {
            if (_networkSettingReference.IsLocal())
                PopulateList();
            else
            {
                _cards = new List<EnvironmentCard>();
            }
        }

        private void Start()
        {
            _environmentDeckReference.Value = this;
            QuantumNetworkManager.OnEnvironmentChanged.AddListener(UpdateList);
            QuantumNetworkManager.OnClientJoin.AddListener(PopulateOnline);
        }

        private void OnDestroy()
        {
            _environmentDeckReference.Reset();
            QuantumNetworkManager.OnEnvironmentChanged.RemoveListener(UpdateList);
            QuantumNetworkManager.OnClientJoin.RemoveListener(PopulateOnline);
        }
        
        public void PopulateList()
        {
            Debug.Log("EVN: Populate List");

            _allCardData.Shuffle();
            for (var i = 0; i < NumberOfCards; i++)
            {
                var cardData = _allCardData[i];
                _cards.Add(new EnvironmentCard(cardData));
            }
        }
        
        public void PopulateOnline()
        {
            PopulateList();
            QuantumNetworkManager.LocalPlayer.ChangeEnvironment(_cards.Select(c => c.NameId).ToArray());
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
            _cards.Add(new EnvironmentCard(_allBossData.First()));
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