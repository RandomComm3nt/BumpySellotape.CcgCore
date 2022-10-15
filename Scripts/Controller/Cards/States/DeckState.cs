using CcgCore.Controller.Cards;
using DreamCcg.Controller.Field;

#nullable enable

namespace BumpySellotape.CcgCore.Controller.Cards.States
{
    public class DeckState : CardState
    {
        public DeckState(CardDeckEngine cardDeckEngine, Card card) : base(cardDeckEngine, card)
        {
        }

        public override bool IsFaceUp => false;
    }
}