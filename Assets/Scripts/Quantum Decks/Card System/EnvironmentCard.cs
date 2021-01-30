using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class EnvironmentCard : Card
    {
        public EnvironmentCard(EnvironmentCardData data) : base(data)
        {
        }

        public void Damage(int value = 1)
        {
            Value -= value;
            Value = Mathf.Max(0, Value);
        }
    }
}