using Quantum_Decks.Environment;
using Quantum_Decks.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quantum_Decks.Card_System
{
    public class EnvironmentCardObject : MonoBehaviour
    {
        [SerializeField] private LocalizationCollection _localizationCollection;

        private EnvironmentCard _card;

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

        public EnvironmentCard Card => _card;

        private void Start()
        {
            _environmentDeck = FindObjectOfType<EnvironmentDeck>();
        }

        public void UpdateCard()
        {
            if (_environmentDeck.Count == 0)
                return;
            
            _card = _environmentDeck.GetByPlayer(_playerId);
            UpdateText();
            _valueTextMesh.text = _card.Value.ToString();
            _valueImage.sprite = _card.ValueBackground;
            _borderImage.sprite = _card.CardFrame;
            _cardImage.sprite = _card.Sprite;

            foreach (Transform child in _fractionTransform)
            {
                Destroy(child.gameObject);
            }

            foreach (var fraction in _card.Fractions)
            {
                SpawnFractionIcon(fraction);
            }
        }

        private void UpdateText()
        {
            _nameTextMesh.text = _localizationCollection.CurrentLocalization.GetTextById(_card.NameId);
            _descriptionTextMesh.text = _localizationCollection.CurrentLocalization.GetTextById(_card.DescriptionId);
            _descriptionTextMesh.fontStyle = _card.IsNeutralised ? FontStyles.Strikethrough : FontStyles.Normal;
        }

        private void SpawnFractionIcon(Fraction fraction)
        {
            var fractionObject = Instantiate(_fractionsPrefab, _fractionTransform);
            fractionObject.GetComponent<FractionObject>().UpdateFraction(fraction);
        }
    }
}