using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Localization
{
    [CreateAssetMenu(menuName = "Quantum/New Localization", fileName = "New Localization [Localization]")]
    public class LocalizationData : ScriptableObject
    {
        [BoxGroup("Language Details"), LabelWidth(120)]
        public string LanguageName =  "English";

        [BoxGroup("Language Details"), LabelWidth(120)]
        public Sprite LanguageSprite;

        [BoxGroup("Environment"), Required, LabelWidth(120)]
        public string revenge = "revenge";

        [BoxGroup("Environment"), Required, LabelWidth(120)]
        public string Ambush = "ambush";

        [BoxGroup("Environment"), Required, LabelWidth(120)]
        public string Defense = "defense";

        [BoxGroup("Environment"), Required, LabelWidth(120)]
        public string Static = "static";

        [BoxGroup("Environment"), Required, LabelWidth(120)]
        public string Shielded = "shielded";

        [BoxGroup("Environment"), Required, LabelWidth(120)]
        public string Deny = "deny";

        [Space(10), BoxGroup("Player Card"), Required, LabelWidth(120)]
        public string Lost = "lost";
    }

    public class LocalizationElement
    {
        private string _id;

        public string ID => _id;
        public string Value;

        public LocalizationElement(string id, string value)
        {
            _id = id;
            Value = value;
        }
    }
}