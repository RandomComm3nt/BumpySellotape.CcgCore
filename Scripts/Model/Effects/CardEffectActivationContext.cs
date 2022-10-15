using BumpySellotape.Events.Controller;
using BumpySellotape.Events.Model.Effects;
using CcgCore.Controller;
using CcgCore.Controller.Actors;
using CcgCore.Controller.Cards;
using System.Collections.Generic;

namespace CcgCore.Model
{
    public class CardEffectActivationContext : ProcessingContext
    {
        public bool wasActionCancelled;

        public List<Card> selectedCards;

        public Card activatedCard;

        public Card targetCard;
        public CardStack targetStack;

        public CardGameControllerBase cardGameController;

        public ActorScope triggerActor;

        public CardEffectActivationContext(GameController gameController) : base(gameController)
        {
        }
    }
}
