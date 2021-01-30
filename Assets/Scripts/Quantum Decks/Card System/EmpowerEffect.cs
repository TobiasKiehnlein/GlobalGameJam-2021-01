using System.Collections;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Empower Effect",
        fileName = "New Empower Effect [Empower Effect]")]
    public class EmpowerEffect : Effect
    {
        [SerializeField] private PlayerCollection _playerCollection;
        [SerializeField] private EnvironmentDeckReference _environmentDeck;
        
        public override IEnumerator ApplyEffect(Player.Player player)
        {
            var otherPlayer = _playerCollection.GetOtherPlayer(player);
            _environmentDeck.Value.GetByPlayer(otherPlayer.PlayerId).Value++;
            
            // TODO: Empower Animation
            
            yield return new WaitForEndOfFrame();
        }
    }
}