using System.Collections.Generic;
using Quantum_Decks.Player;
using UnityEngine;

namespace Shared.Scriptable_References
{
    [CreateAssetMenu(menuName = "Quantum/References/New Player Reference",
        fileName = "New Player Reference [Player Reference]")]
    public class PlayerReference : ScriptableReference<Player>
    {
    }

    public abstract class ScriptableCollection<T> : ScriptableObject
    {
        public List<T> Value = new List<T>();

        public void Add(T value)
        {
            Value.Add(value);
        }

        public void Remove(T value)
        {
            if (Value.Contains(value))
            {
                Value.Remove(value);
            }
        }
    }
}