using System.Collections;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Transmute Effect",
        fileName = "New Transmute Effect [Transmute Effect]")]
    public class TransmuteEffect : Effect
    {
        [SerializeField] private PlayerCollection _playerCollection;
        [SerializeField] private EnvironmentDeckReference _environmentDeck;
        
        public override IEnumerator ApplyEffect(Player.Player player)
        {
            var otherPlayer = _playerCollection.GetOtherPlayer(player);
            _environmentDeck.Value.Transmute(otherPlayer.PlayerId);
            
            // TODO: TRANSMUTATE ANIMATION
            
            yield return new WaitForEndOfFrame();
        }
    }
}