using System.Linq;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class SurgeHandler : MonoBehaviour
    {
        [SerializeField] private PlayerCollection _playerCollection;
        [SerializeField] private BoolReference _isSurgeReference;
        [SerializeField] private Keyword _powerSurgeKeyword;

        public void Update()
        {
            var cards = _playerCollection.Value.Where(p => p.CurrentSelectedCard != null)
                .Select(p => p.CurrentSelectedCard.Card).ToList();
            
            if (cards.Count <= 1)
            {
                _isSurgeReference.Value = false;
                return;
            }

            if (cards.Any(c => c.Keywords.Contains(_powerSurgeKeyword)))
            {
                _isSurgeReference.Value = true;
                return;
            }

            _isSurgeReference.Value = cards.SelectMany(c => c.Fractions).GroupBy(f => f).Any(g => g.Count() > 1);
        }
    }
}