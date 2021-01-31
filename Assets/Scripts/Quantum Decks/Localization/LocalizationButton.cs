using Quantum_Decks.Localization;
using UnityEngine;

public class LocalizationButton : MonoBehaviour
{
    [SerializeField] private LocalizationCollection _localizationCollection;

    public void ChangeLocalization()
    {
        _localizationCollection.NexLocalization();
    }
}