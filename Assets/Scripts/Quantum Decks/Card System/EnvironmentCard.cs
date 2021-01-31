using System.Collections;
using System.Linq;
using Shared.Scriptable_References;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public class EnvironmentCard : Card
    {
        public EnvironmentCard(EnvironmentCardData data) : base(data)
        {
        }

        public IEnumerator Damage(Card card, Card otherPlayer, bool isSurge, Keyword powerSurge, Keyword shielded,
            Keyword elusive, bool isBossFight, BoolReference isVictory)
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
                Debug.Log($"{card.NameId} had powersearch");
                damage = card.Value + otherPlayer.Value;
            }

            if (HasKeyword(shielded) && card.Fractions.Count > 1)
            {
                Debug.Log($"{_data.NameId} was shielded");
                damage = 1;
            }

            if (HasKeyword(elusive))
            {
                Debug.Log($"{_data.NameId} was elusive");
                damage = 1;
            }

            Debug.Log($"{_data.NameId} took {damage} damage");
            Value -= damage;
            Value = Mathf.Max(0, Value);
            isVictory.Value = isBossFight && Value == 0;
            yield return new WaitForEndOfFrame();

            // TODO: Damage Animation
            // TODO: Damage Special Effect
        }
    }
}