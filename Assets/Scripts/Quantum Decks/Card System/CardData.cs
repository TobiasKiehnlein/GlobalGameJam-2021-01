using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    public abstract class CardData : ScriptableObject
    {
        [Required]
        public string NameId;
        [Required]
        public List<Fraction> Fractions;
        public string DescriptionId;
        [Required]
        public int Value;
        public Sprite Sprite;

        public List<EffectData> EffectData;
        public List<Keyword> Keywords;
    }
    
    [Serializable]
    public struct EffectData
    {
        public EffectTrigger Trigger;
        public List<Effect> Effect; 
    }
}
