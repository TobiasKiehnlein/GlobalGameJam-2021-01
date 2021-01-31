using System;
using System.Linq;
using Doozy.Engine.UI;
using Mirror;
using Networking;
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

        [SerializeField] private TextMeshProUGUI _deckCountTextMesh;

        [SerializeField] private NetworkSettingReference _networkSettingReference;

        private CardObject[] _cardObjects;

        public Networking.Player PlayerId => _playerId;
        public CardCollection Deck => _deck;
        public CardCollection Hand => _hand;
        public CardSpawner CardSpawner => _cardSpawner;
        public Transform DropZone => _dropZone;


        [BoxGroup("Player Ui"), SerializeField]
        private UIView _acceptButtonView;

        [BoxGroup] public bool HasAccepted;

        private CardObject _currentSelectedCard;

        [BoxGroup("Card System"), Sirenix.OdinInspector.ShowInInspector]
        public CardObject CurrentSelectedCard => _currentSelectedCard;

        private void Awake()
        {
            _cardObjects = GetComponentsInChildren<CardObject>();
        }

        private void OnEnable()
        {
            QuantumNetworkManager.OnSelectedCardChanged.AddListener(OnSelectedChangeOnline);
            _playerCollection.Add(this);
        }

        private void OnDisable()
        {
            QuantumNetworkManager.OnSelectedCardChanged.RemoveListener(OnSelectedChangeOnline);
            _playerCollection.Remove(this);
        }

        public void Select(CardObject card)
        {
            if (_networkSettingReference.IsLocal() || QuantumNetworkManager.LocalPlayer?.Player == _playerId)
            {
                _currentSelectedCard = card;
                _acceptButtonView.Show();
            }
            
            if (_networkSettingReference.IsOnline() && QuantumNetworkManager.LocalPlayer?.Player == _playerId)
            {
                QuantumNetworkManager.LocalPlayer?.SetSelectedCard(_playerId, card.Card.NameId);
            }
            
  
        }

        public void Deselect()
        {
            if (_networkSettingReference.IsLocal() || QuantumNetworkManager.LocalPlayer?.Player == _playerId)
            {
                _currentSelectedCard = null;
                _acceptButtonView.Hide();
            }
            
            if (_networkSettingReference.IsOnline() && QuantumNetworkManager.LocalPlayer?.Player == _playerId)
            {
                QuantumNetworkManager.LocalPlayer?.SetSelectedCard(_playerId,null);
            }
        }

        public void OnSelectedChangeOnline(Networking.Player playerId, string cardId)
        {
            if (QuantumNetworkManager.LocalPlayer?.Player == playerId || _playerId != playerId)
                return;

            var card = _cardObjects.FirstOrDefault(c => c.Card.NameId == cardId);
            _currentSelectedCard = card;
        }

        private void Update()
        {
            _deckCountTextMesh.text = _deck.Cards.Count.ToString();
        }
    }
}