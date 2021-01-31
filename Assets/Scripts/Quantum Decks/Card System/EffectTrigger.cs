using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/New Effect Trigger",
        fileName = "New Effect Trigger [Effect Trigger]")]
    public class EffectTrigger : ScriptableObject
    {
        public string NameId;
    }
}