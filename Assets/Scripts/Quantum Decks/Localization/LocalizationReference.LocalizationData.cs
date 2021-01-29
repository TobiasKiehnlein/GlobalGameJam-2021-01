using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Localization
{
    public partial class LocalizationReference
    {
        [Serializable]
        public struct LocalizationData
        {
            [Required] public string Id;
            [TextArea] public string Text;
        }
    }
}