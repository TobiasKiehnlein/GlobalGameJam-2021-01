using System.Collections;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Quantum Shift Effect",
        fileName = "New Quantum Shift Effect [Quantum Shift Effect]")]
    public class QuantumShiftEffect : Effect
    {
        [SerializeField] private PlayerCollection _playerCollection;
        
        public override IEnumerator ApplyEffect(Player.Player player)
        {
            var otherPlayer = _playerCollection.GetOtherPlayer(player);
            CardCollection.QuantumShift(player.Deck, otherPlayer.Deck);
            
            // TODO: Quantum Shift Animation
            
            yield return new WaitForEndOfFrame();
        }
    }
}