

using CcgCore.Controller.Cards;
using DreamCcg.Controller.Field;

namespace BumpySellotape.CcgCore.Controller.Cards.States
{
    public class HandState : CardState
    {
        public HandState(CardDeckEngine cardDeckEngine, Card card) : base(cardDeckEngine, card)
        {
        }

        public override void Interact()
        {
            CardDeckEngine.PlayCard(Card);
        }
    }
}