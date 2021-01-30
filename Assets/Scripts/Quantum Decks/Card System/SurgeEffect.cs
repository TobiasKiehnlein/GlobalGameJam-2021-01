using System.Collections;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Effects/New Surge Effect",
        fileName = "New Surge Effect [Surge Effect]")]
    public class SurgeEffect : Effect
    {
        public override IEnumerator ApplyEffect(Player.Player player)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}