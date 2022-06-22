using CcgCore.Model.Cards;
using CcgCore.Model.Parameters;

namespace CcgCore.Controller.Cards
{
    public abstract class CardFactory
    {
        public Card CreateCard(CardDefinition cardDefinition, ParameterScope parent)
        {
            var c = new Card(cardDefinition, parent);
            InitialiseCardAfterCreation(c);
            CreateCardDisplayForNewCard(c);
            return c;
        }

        protected abstract void InitialiseCardAfterCreation(Card card);

        protected abstract void CreateCardDisplayForNewCard(Card card);
    }
}
