using System.Collections;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Burn Effect",
        fileName = "New Burn Effect [Burn Effect]")]
    public class BurnEffect : Effect
    {
        [SerializeField] private CardCollectionReference _voidReference;
        
        public override IEnumerator ApplyEffect(Player.Player player)
        {
            var card = player.Deck.GetRandom();
            player.Deck.Transfer(card, _voidReference.Value);
            Debug.Log($"Burned: {card.NameId} from {player.name}");
            
            // TODO: HERE GOES BURN ANIMATION
            yield return new WaitForEndOfFrame();
        }
    }
}