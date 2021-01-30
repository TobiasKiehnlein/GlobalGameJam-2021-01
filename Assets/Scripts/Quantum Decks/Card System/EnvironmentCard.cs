using System.Collections;
using System.Linq;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class EnvironmentCard : Card
    {
        public EnvironmentCard(EnvironmentCardData data) : base(data)
        {
        }

        public IEnumerator Damage(Card card)
        {
            if (Fractions.Intersect(card.Fractions).Any())
            {
                Value -= card.Value;
            }
            else
            {
                Value--;
            }

            Value = Mathf.Max(0, Value);
            yield return new WaitForEndOfFrame();
            
            // TODO: Damage Animation
            // TODO: Damage Special Effect
        }
    }
}