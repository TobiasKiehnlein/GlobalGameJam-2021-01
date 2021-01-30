using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public abstract class CardData : ScriptableObject
    {
        [Required, BoxGroup("Localization Text"), PropertyOrder(0)]
        public string NameId;
        [Required, BoxGroup("Fraction List")]
        public List<Fraction> Fractions;
        [Required, BoxGroup("Localization Text"), PropertyOrder(0)]
        public string DescriptionId;
        [Required, BoxGroup("Values"), PropertyOrder(1)]
        public int AttackValue;
        [BoxGroup("Art"), PropertyOrder(2)]
        public Sprite Sprite;
        [BoxGroup("Art"), PropertyOrder(2)]
        public Sprite CardFrame;
        [BoxGroup("Art"), PropertyOrder(2)]
        public Sprite ValueBackground;

        [Required, BoxGroup("Effect List")]
        public List<EffectData> EffectData;
        [Required, BoxGroup("Keywords")]
        public List<Keyword> Keywords;
    }
    
    [Serializable]
    public struct EffectData
    {
        public EffectTrigger Trigger;
        public List<Effect> Effect; 
    }
}
