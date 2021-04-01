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
        public CardBase BaseCard => StackedCards.Count > 0 ? StackedCards[0] : null;
        public List<CardBase> StackedCards { get; protected set; }

        public CardStack(FieldRegion region)
            : base(ParameterScopeLevel.Stack, region)
        {
            StackedCards = new List<CardBase>();
            this.region = region;
        }

        public void AddCard(CardBase card)
        {
            StackedCards.Add(card);
            card.SetParentScope(this);
        }

        public void RemoveCard(CardBase card)
        {
            StackedCards.Remove(card);
        }
    }
}
