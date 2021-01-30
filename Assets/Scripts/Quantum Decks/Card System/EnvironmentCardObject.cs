using System;
using Quantum_Decks.Environment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quantum_Decks.Card_System
{
    public class EnvironmentCardObject : MonoBehaviour
    {
        private EnvironmentDeck _environmentDeck;
        [SerializeField] private Networking.Player _playerId;
        [SerializeField] private GameObject _fractionsPrefab;
        [SerializeField] private Transform _fractionTransform;

        [SerializeField] private TextMeshProUGUI _nameTextMesh;
        [SerializeField] private TextMeshProUGUI _valueTextMesh;
        [SerializeField] private TextMeshProUGUI _descriptionTextMesh;
        [SerializeField] private Image _cardImage;
        [SerializeField] private Image _valueImage;
        [SerializeField] private Image _borderImage;

        public EnvironmentCard Card => Card;

        private void Start()
        {
            _environmentDeck = FindObjectOfType<EnvironmentDeck>();
        }

        public void Update()
        {
            if (_environmentDeck.Count > 0)
                UpdateCard();
        }

        public void UpdateCard()
        {
            var card = _environmentDeck.GetByPlayer(_playerId);

            _nameTextMesh.text = card.NameId;
            _descriptionTextMesh.text = card.DescriptionId;
            _valueTextMesh.text = card.Value.ToString();
            _cardImage.sprite = card.Sprite;

            foreach (var fraction in card.Fractions)
            {
                SpawnFractionIcon(fraction);
            }
        }

        private void SpawnFractionIcon(Fraction fraction)
        {
            var fractionObject = Instantiate(_fractionsPrefab, _fractionTransform);
            fractionObject.GetComponent<FractionObject>().UpdateFraction(fraction);
        }
    }
}