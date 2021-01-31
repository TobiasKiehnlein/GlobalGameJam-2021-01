using System;
using System.Linq;
using DG.Tweening;
using Doozy.Engine.UI;
using Shared.Scriptable_References;
using UnityEngine;
using UnityEngine.VFX;

namespace Quantum_Decks.Card_System
{
    public class SurgeHandler : MonoBehaviour
    {
        [SerializeField] private PlayerCollection _playerCollection;
        [SerializeField] private BoolReference _isSurgeReference;
        [SerializeField] private Keyword _powerSurgeKeyword;
        [SerializeField] private UIView _surgeView;
        [SerializeField] private IntReference _surgeValue;
        [SerializeField] private Fraction _fractionLess;
        [SerializeField] private VisualEffect _visualEffect;

        private void Awake()
        {
            _visualEffect.Stop();
        }

        public void RemoveSurge()
        {
            _isSurgeReference.Value = false;
            _surgeView.Hide();
            _visualEffect.Stop();
        }


        public void UpdateSurge()
        {
            var cards = _playerCollection.Value.Where(p => p.CurrentSelectedCard != null)
                .Select(p => p.CurrentSelectedCard.Card).ToList();

            if (cards.Count <= 1)
            {
                RemoveSurge();
                return;
            }

            if (cards.SelectMany(c => c.Fractions).Any(c => c == _fractionLess))
            {
                _surgeValue.Value = cards.Sum(c => c.Value);
                _isSurgeReference.Value = true;
                _surgeView.Show();
                _visualEffect.Play();
                return;
            }

            var group = cards.SelectMany(c => c.Fractions).GroupBy(f => f);
            _isSurgeReference.Value = group.Any(g => g.Count() > 1);
            if (_isSurgeReference.Value)
            {
                _surgeValue.Value = cards.Sum(c => c.Value);
                _surgeView.Show();
                _visualEffect.Play();
            }
            else
            {
                RemoveSurge();
            }
        }
    }
}