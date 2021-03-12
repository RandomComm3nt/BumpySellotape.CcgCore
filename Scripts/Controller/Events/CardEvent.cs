using CcgCore.Controller.Cards;

namespace CcgCore.Controller.Events
{
    public class CardEvent : CardGameEvent
    {
        public enum CardEventType
        {
            CardAdded = 0,
        }

        public CardEvent(CardEventType eventType) : base(eventType.ToString())
        {
        }
    }
}
