using CcgCore.Controller.Events;
using CcgCore.Model;
using CcgCore.Model.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace CcgCore.Controller.Cards
{
    public class FieldRegion<TCard, TCardDefinition> : ParameterScope
        where TCard : CardBase<TCardDefinition>
        where TCardDefinition : CardDefinitionBase
    {
        public delegate void RegionChange();
        public event RegionChange OnCardAdded;

        private readonly List<CardStack<TCard, TCardDefinition>> cardStacks;

        public FieldRegion(CardGameControllerBase cardGameController, ParameterScope parentScope = null)
            : base(ParameterScopeLevel.Region, parentScope ?? cardGameController)
        {
            cardStacks = new List<CardStack<TCard, TCardDefinition>>();
        }

        public void AddCard(TCardDefinition cardDefinition)
        {
            var card  = CardFactory.cardFactory.CreateCard<TCard, TCardDefinition>(cardDefinition, null);
            AddCardToNewStack(card);
        }

        public void AddCard(TCard card)
        {
            AddCardToNewStack(card);
        }

        private void AddCardToNewStack(TCard card)
        {
            var stack = new CardStack<TCard, TCardDefinition>(this);
            cardStacks.Add(stack);
            stack.AddCard(card);
            CardEvent cardGameEvent = new CardEvent(CardEvent.CardEventType.CardAdded);
            card.RaiseEvent(cardGameEvent);

            if (cardGameEvent.IsCancelled)
            {
                RemoveChild(stack);
                cardStacks.Remove(stack);
                return;
            }

            OnCardAdded?.Invoke();
        }

        public void RemoveCard(TCard card)
        {

        }

        public List<CardStack<TCard, TCardDefinition>> FindCardStacks()
        {
            return cardStacks
                .ToList();
        }

        public List<TCard> FindCards(bool searchInStacks = false, TCardDefinition cardDefinition = null)
        {
            return cardStacks
                .SelectMany(s => searchInStacks ? s.StackedCards : s.StackedCards.Take(1))
                .Where(c => !cardDefinition || c.CardDefinition == cardDefinition)
                .ToList();
        }

        public int GetCardQuantity(TCardDefinition cardDefinition)
        {
            return 0;// cardStacks.Where(cs => cs.CardDefinition == cardDefinition).Sum(cs => cs.Quantity);
        }
    }
}
