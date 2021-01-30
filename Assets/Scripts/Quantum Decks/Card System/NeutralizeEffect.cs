using System.Collections;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Neutralize Effect",
        fileName = "New Neutralize Effect [Neutralize Effect]")]
    public class NeutralizeEffect : Effect
    {
        [SerializeField] private EnvironmentDeckReference _environmentDeck;
        
        public override IEnumerator ApplyEffect(Player.Player player)
        {
            _environmentDeck.Value.GetByPlayer(player.PlayerId).Neutralize();
            yield return new WaitForEndOfFrame();
        }
    }
}