using CcgCore.Controller.Cards;
using DreamCcg.Controller.Field;

namespace BumpySellotape.CcgCore.Controller.Cards.States
{
    public class TransitioningState : CardState
    {
        public TransitioningState(CardDeckEngine cardDeckEngine, Card card) : base(cardDeckEngine, card)
        {
        }
    }
}