using Sirenix.OdinInspector;

namespace Quantum_Decks.Card_System
{
    public class PlayerCard : Card
    {
        [BoxGroup, PropertyOrder(2)]
        public int Duration;

        private readonly int _startDuration;
        
        public PlayerCard(PlayerCardData data) : base(data)
        {
            _startDuration = data.Duration;
            Duration = data.Duration;
        }

        public void Reset()
        {
            _isNeutralised = false;
            Duration = _startDuration;
        }
    }
}