using CcgCore.Controller.Actors;

namespace CcgCore.Model.Cards
{
    public abstract class CardDefinitionModule
    {
        public virtual bool ValidateCardCanBeUsed(Actor actor) => true;
    }
}
