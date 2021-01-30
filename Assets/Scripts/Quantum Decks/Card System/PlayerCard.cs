using Sirenix.OdinInspector;

namespace Quantum_Decks.Card_System
{
    public class PlayerCard : Card
    {
        [BoxGroup, PropertyOrder(2)]
        public int Duration;
        
        public PlayerCard(PlayerCardData data) : base(data)
        {
            Duration = data.Duration;
        }
    }
}