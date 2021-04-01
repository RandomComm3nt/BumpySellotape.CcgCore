using CcgCore.Controller.Events;
using CcgCore.Model.Cards;
using CcgCore.Model.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace CcgCore.Controller.Cards
{
    public class FieldRegion : ParameterScope
    {
        public delegate void RegionChange();
        public event RegionChange OnCardAdded;

        private readonly List<CardStack> cardStacks;

        public FieldRegion(CardGameControllerBase cardGameController, ParameterScope parentScope = null)
            : base(ParameterScopeLevel.Region, parentScope ?? cardGameController)
        {
            cardStacks = new List<CardStack>();
        }

        public void AddCard(CardDefinition cardDefinition)
        {
            var card  = CardFactory.cardFactory.CreateCard<CardBase>(cardDefinition, null);
            AddCardToNewStack(card);
        }

        public void AddCard(CardBase card)
        {
            AddCardToNewStack(card);
        }

        private void AddCardToNewStack(CardBase card)
        {
            var stack = new CardStack(this);
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

        public void RemoveCard(CardBase card)
        {

        }

        public List<CardStack> FindCardStacks()
        {
            return cardStacks
                .ToList();
        }

        public List<CardBase> FindCards(bool searchInStacks = false, CardDefinition cardDefinition = null)
        {
            return cardStacks
                .SelectMany(s => searchInStacks ? s.StackedCards : s.StackedCards.Take(1))
                .Where(c => !cardDefinition || c.CardDefinition == cardDefinition)
                .ToList();
        }

        public int GetCardQuantity(CardDefinition cardDefinition)
        {
            return 0;// cardStacks.Where(cs => cs.CardDefinition == cardDefinition).Sum(cs => cs.Quantity);
        }
    }
}
