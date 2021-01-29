using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quantum_Decks.Localization
{
    [CreateAssetMenu(menuName = "Quantum/New Localization", fileName = "New Localization [Localization]")]
    public partial class LocalizationReference : ScriptableObject
    {
        [SerializeField] private List<LocalizationData> _localization = new List<LocalizationData>();

        public string GetTextById(string id)
        {
            if (id.Length == 0)
                return "";

            var localization = _localization.Where(l => l.Id == id).ToList();
            if (localization.Count == 0)
            {
                throw new Exception($"{name} could not find id of {id}");
            }

            if (localization.Count > 1)
            {
                throw new Exception($"{name} has more than one entry with the id of {id}");
            }

            return localization[0].Text;
        }
    }
}