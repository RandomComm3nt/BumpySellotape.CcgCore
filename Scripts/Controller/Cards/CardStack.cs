using CcgCore.Model;
using CcgCore.Model.Parameters;
using System;
using System.Collections.Generic;

namespace CcgCore.Controller.Cards
{
    public class CardStack<TCard, TCardDefinition> : ParameterScope
        where TCard : CardBase<TCardDefinition>
        where TCardDefinition : CardDefinitionBase
    {
        private FieldRegion<TCard, TCardDefinition> region;

        /// <summary>
        /// Returns the bottom card of the stack, or null if the stack is empty
        /// </summary>
        public TCard BaseCard => StackedCards.Count > 0 ? StackedCards[0] : null;
        public List<TCard> StackedCards { get; protected set; }

        public CardStack(FieldRegion<TCard, TCardDefinition> region)
            : base(ParameterScopeLevel.Stack, region)
        {
            StackedCards = new List<TCard>();
        }

        public void AddCard(TCard card)
        {
            StackedCards.Add(card);
            card.SetParentScope(this);
        }
    }
}
