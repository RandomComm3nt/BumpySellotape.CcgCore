using CcgCore.Controller.Cards;
using DreamCcg.Controller.Field;

#nullable enable

namespace BumpySellotape.CcgCore.Controller.Cards.States
{
    public abstract class CardState
    {
        public CardDeckEngine CardDeckEngine { get; }
        public Card Card { get; private set; }

        public CardState(CardDeckEngine cardDeckEngine, Card card)
        {
            CardDeckEngine = cardDeckEngine;
            Card = card;
        }

        public virtual void EnterState(Card card)
        {
            Card = card;
        }

        public virtual void Interact()
        { }

        public virtual bool IsFaceUp => true;
    }
}