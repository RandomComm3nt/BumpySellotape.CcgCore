using CcgCore.Model.Cards;
using CcgCore.Model.Parameters;

namespace CcgCore.Controller.Cards
{
    public class CardFactory
    {
        public Card CreateCard(CardDefinition cardDefinition, ParameterScope parent)
        {
            return new Card(cardDefinition, parent);
        }

        protected virtual void InitialiseCardAfterCreation(Card card)
        {

        }

        protected virtual void CreateCardDisplayForNewCard(Card card)
        {

        }
    }
}
