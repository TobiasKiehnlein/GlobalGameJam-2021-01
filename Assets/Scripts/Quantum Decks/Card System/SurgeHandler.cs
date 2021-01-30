using System.Linq;
using Doozy.Engine.UI;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class SurgeHandler : MonoBehaviour
    {
        [SerializeField] private PlayerCollection _playerCollection;
        [SerializeField] private BoolReference _isSurgeReference;
        [SerializeField] private Keyword _powerSurgeKeyword;
        [SerializeField] private UIView _surgeView;
        [SerializeField] private IntReference _surgeValue;

        public void Update()
        {
            var cards = _playerCollection.Value.Where(p => p.CurrentSelectedCard != null)
                .Select(p => p.CurrentSelectedCard.Card).ToList();
            
            if (cards.Count <= 1)
            {
                _isSurgeReference.Value = false;
                _surgeView.Hide();
                return;
            }

            if (cards.Any(c => c.Keywords.Contains(_powerSurgeKeyword)))
            {
                _isSurgeReference.Value = true;
                _surgeView.Show();
                _surgeValue.Value = cards.Sum(c => c.Value);
                return;
            }

            _isSurgeReference.Value = cards.SelectMany(c => c.Fractions).GroupBy(f => f).Any(g => g.Count() > 1);
            if (_isSurgeReference.Value)
            {
                _surgeView.Show();
                _surgeValue.Value = cards.Sum(c => c.Value);
            }
            else
            {
                _surgeView.Hide();
            }
        }
    }
}