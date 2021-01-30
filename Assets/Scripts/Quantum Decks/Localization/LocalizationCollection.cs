using System.Linq;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Localization
{
    [CreateAssetMenu(menuName = "Quantum/Collection/New Localization Collection",
        fileName = "New Localization Collection [New Localization Collection]")]
    public class LocalizationCollection : ScriptableCollection<LocalizationData>
    {
        [ShowInInspector]
        public LocalizationData CurrentLocalization => Value.FirstOrDefault();
    }
}