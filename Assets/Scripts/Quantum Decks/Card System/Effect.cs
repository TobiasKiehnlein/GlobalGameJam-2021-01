using System.Collections;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public abstract class Effect : ScriptableObject
    {
        public abstract IEnumerator ApplyEffect(Player.Player player);
    }
}