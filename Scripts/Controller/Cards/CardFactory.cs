using CcgCore.Model;
using CcgCore.Model.Parameters;
using System;

namespace CcgCore.Controller.Cards
{
    // singleton pattern isn't the best, but might be the nicest option here to deal with generics
    public abstract class CardFactory
    {
        public static CardFactory cardFactory; // TECH DEBT - public access

        public abstract TCard CreateCard<TCard, TCardDefinition>(TCardDefinition cardDefinition, ParameterScope parent)
            where TCard : CardBase<TCardDefinition>
            where TCardDefinition : CardDefinitionBase;

        public CardFactory()
        {
            cardFactory = this;
        }
    }

    public abstract class DerivedCardFactory<TCard, TCardDefinition> : CardFactory
        where TCard : CardBase<TCardDefinition>
        where TCardDefinition : CardDefinitionBase
    {
        public sealed override TCard2 CreateCard<TCard2, TCardDefinition2>(TCardDefinition2 cardDefinition, ParameterScope parent)
        {
            if (!(cardDefinition is TCardDefinition2))
                throw new Exception("Input cardDefinition is not the correct type");
            var card = CreateCard(cardDefinition as TCardDefinition, parent);
            if (!(card is TCard2))
                throw new Exception("Created card is not the correct type");
            return card as TCard2;
        }

        protected abstract TCard CreateCard(TCardDefinition cardDefinition, ParameterScope parent);
    }
}
