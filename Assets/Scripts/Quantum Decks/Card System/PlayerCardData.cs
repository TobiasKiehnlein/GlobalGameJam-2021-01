using Sirenix.OdinInspector;
using UnityEngine;

namespace Quantum_Decks.Card_System
{
    [CreateAssetMenu(menuName = "Quantum/Card System/Player/New Empty Card", fileName = "New Card Data [Player Card Data]")]
    public class PlayerCardData : CardData
    {
        [MinValue(1), BoxGroup("Values"), PropertyOrder(1)]
        public int Duration = 1;
    }
}