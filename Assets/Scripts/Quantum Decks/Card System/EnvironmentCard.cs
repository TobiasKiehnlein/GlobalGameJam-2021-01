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

        public IEnumerator Damage(Card card, Card otherPlayer , bool isSurge, Keyword powerSurge, Keyword shielded, Keyword elusive)
        {
            var damage = card.Value;

            if (isSurge)
            {
                damage = card.Value + otherPlayer.Value;
            }
            
            if (!Fractions.Intersect(card.Fractions).Any())
            {
                damage = 1;
            }
            
            if (isSurge && card.HasKeyword(powerSurge))
            {
                damage +=  otherPlayer.Value;
            }

            if (HasKeyword(shielded) && card.Fractions.Count > 1)
            {
                damage = 1;
            }

            if (HasKeyword(elusive))
            {
                damage = 1;
            }

            Value -= damage;
            Value = Mathf.Max(0, Value);
            yield return new WaitForEndOfFrame();

            // TODO: Damage Animation
            // TODO: Damage Special Effect
        }
    }
}