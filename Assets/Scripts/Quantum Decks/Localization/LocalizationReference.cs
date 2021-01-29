using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Localization
{
    [CreateAssetMenu(menuName = "Quantum/New Localization", fileName = "New Localization [Localization]")]
    public partial class LocalizationReference : ScriptableObject
    {
        [Header("Environment")] [Required] public string revenge = "revenge";
        [Required] public string ambush = "ambush";
        [Required] public string defense = "defense";
        [Required] public string sstatic = "static";
        [Required] public string shielded = "shielded";
        [Required] public string deny = "deny";

        [Space(10)] [Header("Playercard")] [Required]
        public string lost = "lost";
    }
}