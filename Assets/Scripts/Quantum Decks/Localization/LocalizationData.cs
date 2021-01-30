using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Localization
{
    [CreateAssetMenu(menuName = "Quantum/New Localization", fileName = "New Localization [Localization]")]
    public class LocalizationData : ScriptableObject
    {
        [BoxGroup("Language Details"), LabelWidth(120)]
        public string LanguageName = "English";

        [BoxGroup("Language Details"), LabelWidth(120)]
        public Sprite LanguageSprite;

        public List<LocalizationElement> _localisation = new List<LocalizationElement>();

        public string GetTextById(string id)
        {
            var text = _localisation.FirstOrDefault(l => l.Id == id);
            if (string.IsNullOrEmpty(text.Id))
            {
                Debug.LogWarning($"Localization [{LanguageName}] has no id [{id}]");
                return "TEXT_MISSING";
            }
            return text.Value;
        }

        [Button(ButtonSizes.Medium)]
        public void ValidateList()
        {
            for (var i = 0; i < _localisation.Count; i++)
            {
                var localizationElement = _localisation[i];
                if (localizationElement.Id.Length == 0)
                {
                    Debug.LogError($"Localization on index [{i}] had no id");
                    break;
                }
            }

            if (_localisation.GroupBy(l => l.Id).Any(g => g.Count() > 1))
                Debug.LogError("Localization is not allowed to have multiple Ids of the same name");
        }
    }

    [Serializable]
    public struct LocalizationElement
    {
        [Required] public string Id;
        public string Value;
    }
}