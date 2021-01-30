using System.Collections.Generic;
using System.Linq;
using Quantum_Decks.Localization;
using TMPro;
using UnityEngine;

namespace Quantum_Decks.Game
{
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField] private LocalizationCollection _localizationCollection;

        private List<TextMeshLocalizationData> _allLocalizationText;

        private void Awake()
        {
            var allText = FindObjectsOfType<TextMeshProUGUI>().ToList();
            allText = allText.Where(a => a.text.Length > 0).ToList();

            _allLocalizationText = allText.Select(a => new TextMeshLocalizationData {SavedId = a.text, TextMesh = a})
                .ToList();
            UpdateText();
        }

        public void UpdateText()
        {
            foreach (var localization in _allLocalizationText)
            {
                localization.TextMesh.text =
                    _localizationCollection.CurrentLocalization.GetTextById(localization.SavedId);
            }
        }
    }

    public struct TextMeshLocalizationData
    {
        public string SavedId;
        public TextMeshProUGUI TextMesh;
    }
}