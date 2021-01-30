using System;
using DG.Tweening;
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
        [SerializeField] private GameObject _cardDummy;
        [SerializeField] private Transform _fractionTransform;

        [SerializeField] private TextMeshProUGUI _nameTextMesh;
        [SerializeField] private TextMeshProUGUI _valueTextMesh;
        [SerializeField] private TextMeshProUGUI _descriptionTextMesh;
        [SerializeField] private Image _cardImage;
        [SerializeField] private Image _valueImage;
        [SerializeField] private Image _borderImage;

        [ShowInInspector] public Player.Player Owner => _owner;

        public int SavedSiblingIndex => _savedSiblingIndex;

        private HandAnimations _handAnimations;

        public Card Card => _card;

        private void Awake()
        {
            _savedSiblingIndex = transform.GetSiblingIndex();
        }

        private void Start()
        {
            _handAnimations = GetComponentInParent<HandAnimations>();
            _owner = _handAnimations.GetPlayer();
        }

        public void UpdateCard(Player.Player owner, Card card)
        {
            _owner = owner;
            _card = card;

            _valueImage.sprite = card.ValueBackground;
            _borderImage.sprite = card.CardFrame;
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
            if (_owner.HasAccepted)
                return;

            if (_owner.CurrentSelectedCard == this)
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
            // if (Owner.CurrentSelectedCard != null)
            // {
            //     Owner.CurrentSelectedCard.transform.SetParent(_owner.Hand.transform);
            //     Owner.CurrentSelectedCard.transform.SetSiblingIndex(Owner.CurrentSelectedCard.SavedSiblingIndex);
            // }

            // var go = Instantiate(_cardDummy);
            // go.transform.SetParent(_owner.Hand.transform);
            // go.transform.SetSiblingIndex(Owner?.CurrentSelectedCard?.SavedSiblingIndex ?? 0);


            // transform.SetParent(_owner.DropZone);
            // ((RectTransform) transform).anchoredPosition = Vector2.zero;
            // transform.position = _owner.DropZone.position;
            // transform.DOMove(_owner.DropZone.position, .5f);
            // transform.DOScale(new Vector3(.7f, .7f, .7f), .5f);

            _handAnimations.SelectIndex(transform.GetSiblingIndex());
            _owner.Select(this);
        }

        public void OnDeselect()
        {
            // transform.DOScale(Vector3.one, .5f);
            // transform.SetParent(_owner.Hand.transform);
            // Owner.CurrentSelectedCard.transform.SetSiblingIndex(Owner.CurrentSelectedCard.SavedSiblingIndex);

            _handAnimations.Deselect();
            _owner.Deselect();
        }
    }
}