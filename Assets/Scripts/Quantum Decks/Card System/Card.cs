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

        [BoxGroup, ShowInInspector, PropertyOrder(0)]
        public string NameId => _data.NameId;
        public string DescriptionId => _data.DescriptionId;
        public List<Fraction> Fractions => _data.Fractions;
        public Sprite Sprite => _data.Sprite;
        public Sprite ValueBackground => _data.ValueBackground;
        public Sprite CardFrame => _data.CardFrame;
        public List<Keyword> Keywords => _data.Keywords;

        [BoxGroup, PropertyOrder(1)]
        public int Value;

        protected Card(CardData data)
        {
            _data = data;
            Value = data.AttackValue;
        }

        public bool HasKeyword(Keyword keyword)
        {
            return Keywords.Contains(keyword);
        }
        
        public IEnumerator ApplyEffects(EffectTrigger trigger)
        {
            foreach (var effect in GetEffects(trigger))
            {
                yield return effect.ApplyEffect();
            }
        }

        private List<Effect> GetEffects(EffectTrigger trigger)
        {
            return _data.EffectData.Where(e => e.Trigger == trigger).SelectMany(e => e.Effect).ToList();
        }

     
    }
}