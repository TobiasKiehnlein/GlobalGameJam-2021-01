using System.Collections;
using System.Linq;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Rescue Effect",
        fileName = "New Rescue Effect [Rescue Effect]")]
    public class RescueEffect : Effect
    {
        [SerializeField] private CardCollectionReference _voidReference;

        public override IEnumerator ApplyEffect(Player.Player player)
        {
            if (_voidReference.Value.Cards.Any())
                yield break;

            var card = _voidReference.Value.GetRandom();

            _voidReference.Value.Transfer(card, player.Deck);
            player.Deck.Shuffle();

            // TODO: RESCUE ANIMATION
            yield return new WaitForEndOfFrame();
        }
    }
}