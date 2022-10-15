using BumpySellotape.Events.Model.Effects;
using BumpySellotape.TurnBased.Controller.Actors;
using CcgCore.Controller.Cards;

namespace CcgCore.Model.Cards
{
    public abstract class CardDefinitionModule
    {
        public virtual bool ValidateCardCanBeUsed(Actor actor) => true;

        public virtual void ActivateCard(ProcessingContext context, Card thisCard)
        { }
    }
}
