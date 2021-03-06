﻿using System;
using System.Linq;
using Doozy.Engine.UI;
using Quantum_Decks.Card_System;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Quantum_Decks.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField, EnumToggleButtons] private Networking.Player _playerId;

        [SerializeField, Required, BoxGroup("References")]
        private PlayerCollection _playerCollection;

        [SerializeField, BoxGroup("Card System")]
        private CardCollection _deck;

        [SerializeField, BoxGroup("Card System")]
        private CardCollection _hand;

        [SerializeField, BoxGroup("Card System")]
        private CardSpawner _cardSpawner;

        [SerializeField, BoxGroup("Card System")]
        private Transform _dropZone;

        private HandAnimations _animations;

        [SerializeField] private TextMeshProUGUI _deckCountTextMesh;

        public Networking.Player PlayerId => _playerId;
        public CardCollection Deck => _deck;
        public CardCollection Hand => _hand;
        public CardSpawner CardSpawner => _cardSpawner;
        public Transform DropZone => _dropZone;
        
        public HandAnimations Animations => _animations;
        
        [BoxGroup("Player Ui"), SerializeField]
        private UIView _acceptButtonView;

        [BoxGroup] public bool HasAccepted;

        private CardObject _currentSelectedCard;

        [BoxGroup("Card System"), ShowInInspector]
        public CardObject CurrentSelectedCard => _currentSelectedCard;

        private void OnEnable()
        {
            _playerCollection.Add(this);
            _animations = GetComponentInChildren<HandAnimations>();
        }

        private void OnDisable()
        {
            _playerCollection.Remove(this);
        }

        public void ResetCurrentSelectedCard()
        {
            _currentSelectedCard = null;
        }

        public void Accept()
        {
            HasAccepted = true;
        }

        public void Select(CardObject card)
        {
            _currentSelectedCard = card;
            _acceptButtonView.Show();
        }

        public void Deselect()
        {
            _currentSelectedCard = null;
            _acceptButtonView.Hide();
        }

        private void Update()
        {
            _deckCountTextMesh.text = _deck.Cards.Count().ToString();
        }
    }
}