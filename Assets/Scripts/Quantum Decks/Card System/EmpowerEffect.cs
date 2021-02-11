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
            if (_environmentDeck.Value.Count <= 1)
            {
                yield break;
            }
            
            var otherPlayer = _playerCollection.GetOtherPlayer(player);
            _environmentDeck.Value.GetByPlayer(otherPlayer.PlayerId).Value++;
            FeedbackManager.Instance.TriggerFeedback(player.PlayerId, 1);
            
            // TODO: Empower Animation
            
            yield return new WaitForEndOfFrame();
        }
    }
}