using System.Collections;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Transmute Effect",
        fileName = "New Transmute Effect [Transmute Effect]")]
    public class TransmuteEffect : Effect
    {
        public override IEnumerator ApplyEffect(Player.Player player)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}