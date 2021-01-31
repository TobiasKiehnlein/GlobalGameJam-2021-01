using System;
using System.Collections.Generic;
using System.Linq;
using Networking;
using Quantum_Decks.Audio;
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

        [ShowInInspector]
        private List<EnvironmentCard> _cards = new List<EnvironmentCard>();

        [SerializeField] private EnvironmentDeckReference _environmentDeckReference;
        [SerializeField] private NetworkSettingReference _networkSettingReference;

        [SerializeField] private int NumberOfCards = 17;
        [SerializeField] private BoolReference _isBossFight;

        private EnvironmentCardObject[] _environmentCardObjects;
        
        
        public int Count => _cards.Count;

        private void Awake()
        {
            _environmentCardObjects = FindObjectsOfType<EnvironmentCardObject>();
            _isBossFight.Reset();
            
            if (_networkSettingReference.IsLocal())
                PopulateList();
            else
            {
                _cards = new List<EnvironmentCard>();
            }
        }

        public void UpdateAll()
        {
            _environmentCardObjects.ForEach(e => e.UpdateCard());
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

        [Button(ButtonSizes.Medium)]
        public void SpawnBoss()
        {
            _cards.Clear();
            AudioManager.Instance.SwitchToBattle();
            _allBossData.Shuffle();
            _cards.Add(new EnvironmentCard(_allBossData.First()));
            UpdateAll();
            _isBossFight.Value = true;
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