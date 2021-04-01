using CcgCore.Model.Cards;
using CcgCore.Model.Parameters;
using System;

namespace CcgCore.Controller.Cards
{
    // singleton pattern isn't the best, but might be the nicest option here to deal with generics
    public abstract class CardFactory
    {
        public static CardFactory cardFactory; // TECH DEBT - public access

        public abstract TCard CreateCard<TCard>(CardDefinition cardDefinition, ParameterScope parent)
            where TCard : CardBase;

        public CardFactory()
        {
            cardFactory = this;
        }
    }

    public abstract class DerivedCardFactory<TCard> : CardFactory
        where TCard : CardBase
    {
        public sealed override TCard2 CreateCard<TCard2>(CardDefinition cardDefinition, ParameterScope parent)
        {
            if (!(cardDefinition is CardDefinition))
                throw new Exception("Input cardDefinition is not the correct type");
            var card = CreateCard(cardDefinition as CardDefinition, parent);
            if (!(card is TCard2))
                throw new Exception("Created card is not the correct type");
            return card as TCard2;
        }

        protected abstract TCard CreateCard(CardDefinition cardDefinition, ParameterScope parent);
    }
}
