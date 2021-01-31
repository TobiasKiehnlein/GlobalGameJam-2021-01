using Shared.Scriptable_References;
using TMPro;
using UnityEngine;

namespace Quantum_Decks.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class EndScreenLocalization : MonoBehaviour
    {
        [SerializeField] private LocalizationCollection _localizationCollection;
        [SerializeField] private BoolReference _isVictory;

        public string _victoryId;
        public string _gameOverId;


        private void Awake()
        {
            var textMesh = GetComponent<TextMeshProUGUI>();
            textMesh.text =
                _localizationCollection.CurrentLocalization.GetTextById(_isVictory.Value ? _victoryId : _gameOverId);
        }
    }
}