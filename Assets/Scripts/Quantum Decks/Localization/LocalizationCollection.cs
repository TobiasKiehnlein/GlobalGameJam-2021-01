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
        public delegate void LocalizationEvent();

        public event LocalizationEvent OnLocalizationChanged;
        
        [ShowInInspector]
        public LocalizationData CurrentLocalization => Value.FirstOrDefault();

        [Button(ButtonSizes.Medium)]
        public void SetLocalization(LocalizationData data)
        {
            if (Value.Contains(data))
            {
                Value.Remove(data);
            }
            
            Value.Insert(0, data);
            OnLocalizationChanged?.Invoke();
        }

        [Button(ButtonSizes.Medium)]
        public void NexLocalization()
        {
            var localization = Value.LastOrDefault();
            if (localization)
            {
                SetLocalization(localization);
            }
        }
    }
}