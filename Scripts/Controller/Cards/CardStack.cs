using CcgCore.Model.Parameters;
using System.Collections.Generic;

namespace CcgCore.Controller.Cards
{
    public class CardStack : ParameterScope
    {
        private FieldRegion region;

        /// <summary>
        /// Returns the bottom card of the stack, or null if the stack is empty
        /// </summary>
        public Card BaseCard => StackedCards.Count > 0 ? StackedCards[0] : null;
        public List<Card> StackedCards { get; protected set; }

        public CardStack(FieldRegion region)
            : base(ParameterScopeLevel.Stack, region)
        {
            StackedCards = new List<Card>();
            this.region = region;
        }

        public void AddCard(Card card)
        {
            StackedCards.Add(card);
            card.SetParentScope(this);
        }

        public void RemoveCard(Card card)
        {
            StackedCards.Remove(card);
        }
    }
}
