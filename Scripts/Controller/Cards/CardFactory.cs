using CcgCore.Model.Cards;
using CcgCore.Model.Parameters;

namespace CcgCore.Controller.Cards
{
    // singleton pattern isn't the best, but might be the nicest option here to deal with generics
    public class CardFactory
    {
        public static CardFactory cardFactory; // TECH DEBT - public access

        public Card CreateCard(CardDefinition cardDefinition, ParameterScope parent)
        {
            return new Card(cardDefinition, parent);
        }

        public CardFactory()
        {
            cardFactory = this;
        }
    }
}
