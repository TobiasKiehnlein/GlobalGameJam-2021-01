using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public abstract class Card
    {
        protected CardData _data;

        public delegate void CardEvent();

        public event CardEvent OnCardChanged;

        [BoxGroup, ShowInInspector, PropertyOrder(0)]
        public string NameId => _data.NameId;

        public string DescriptionId => _data.DescriptionId;
        public List<Fraction> Fractions => _data.Fractions;
        public Sprite Sprite => _data.Sprite;
        public Sprite ValueBackground => _data.ValueBackground;
        public Sprite CardFrame => _data.CardFrame;
        public List<Keyword> Keywords => _data.Keywords;

        private bool _isNeutralised;
        public bool IsNeutralised => _isNeutralised;

        [BoxGroup, PropertyOrder(1)] public int Value;

        protected Card(CardData data)
        {
            _data = data;
            Value = data.AttackValue;
        }

        public bool HasKeyword(Keyword keyword)
        {
            return Keywords.Contains(keyword);
        }

        public IEnumerator ApplyEffects(EffectTrigger trigger, Player.Player player)
        {
            foreach (var effect in GetEffects(trigger))
            {
                Debug.Log(NameId + " " + effect.name);
                yield return effect.ApplyEffect(player);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void Neutralize()
        {
            _isNeutralised = true;
            OnCardChanged?.Invoke();
        }

        private List<Effect> GetEffects(EffectTrigger trigger)
        {
            return _isNeutralised
                ? new List<Effect>()
                : _data.EffectData.Where(e => e.Trigger == trigger).SelectMany(e => e.Effect).ToList();
        }
    }
}