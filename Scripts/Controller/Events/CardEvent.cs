using CcgCore.Controller.Cards;

namespace CcgCore.Controller.Events
{
    public class CardEvent : CardGameEvent
    {
        // TECH DEBT - not mapped to other enum
        public enum CardEventType
        {
            CardAdded = 0,
        }

        public CardEvent(CardEventType eventType) : base(eventType.ToString())
        {
        }
    }
}
