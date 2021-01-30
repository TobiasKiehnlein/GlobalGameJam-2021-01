using System.Linq;
using Quantum_Decks.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shared.Scriptable_References
{
    [CreateAssetMenu(menuName = "Quantum/Collection/New Player Collection",
        fileName = "New Player Collection [Player Collection]")]
    public class PlayerCollection : ScriptableCollection<Player>
    {
        [BoxGroup("Debug"), ShowInInspector] public Player CurrentPlayer => Value.FirstOrDefault();

        [BoxGroup("Debug"), Button(ButtonSizes.Medium)]
        public void SwitchPlayers()
        {
            var first = Value.FirstOrDefault();
            if (first == null)
                return;

            Remove(first);
            Add(first);
        }

        [BoxGroup("Debug"), ShowInInspector] public bool AllPlayerHaveAccepted => Value.All(p => p.HasAccepted);


        [BoxGroup("Debug"), Button(ButtonSizes.Medium)]
        public void ResetAcceptedState()
        {
            Value.ForEach(p => p.HasAccepted = false);
        }
    }
}