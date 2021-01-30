using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quantum_Decks.Card_System
{
    public class CardObject : MonoBehaviour, IPointerClickHandler
    {
        private Player.Player _owner;
        private Card _card;
        private int _savedSiblingIndex;
        [SerializeField] private GameObject _fractionsPrefab;
        [SerializeField] private Transform _fractionTransform;

        [SerializeField] private TextMeshProUGUI _nameTextMesh;
        [SerializeField] private TextMeshProUGUI _valueTextMesh;
        [SerializeField] private TextMeshProUGUI _descriptionTextMesh;
        [SerializeField] private Image _cardImage;
        [SerializeField] private Image _valueImage;
        [SerializeField] private Image _borderImage;

        [ShowInInspector] public Player.Player Owner => _owner;

        public int SavedSiblingIndex => _savedSiblingIndex;

        public Card Card => _card;

        private void Awake()
        {
            _savedSiblingIndex = transform.GetSiblingIndex();
        }

        public void UpdateCard(Player.Player owner, Card card)
        {
            _owner = owner;
            _card = card;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Owner.CurrentSelectedCard != null && Owner.CurrentSelectedCard == this)
            {
                OnDeselect();
            }
            else
            {
                OnSelect();
            }
        }

        public void OnSelect()
        {
            if (Owner.CurrentSelectedCard != null)
            {
                Owner.CurrentSelectedCard.transform.SetParent(_owner.Hand.transform);
                Owner.CurrentSelectedCard.transform.SetSiblingIndex(Owner.CurrentSelectedCard.SavedSiblingIndex);
            }
        
            transform.SetParent(_owner.DropZone);
            transform.position = _owner.DropZone.position;
            _owner.Select(this);
        }

        public void OnDeselect()
        {
            transform.SetParent(_owner.Hand.transform);
            Owner.CurrentSelectedCard.transform.SetSiblingIndex(Owner.CurrentSelectedCard.SavedSiblingIndex);
            Owner.Deselect();
        }
    }
}