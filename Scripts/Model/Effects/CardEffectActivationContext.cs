using BumpySellotape.CcgCore.Model.Effects;
using CcgCore.Controller;
using CcgCore.Controller.Actors;
using CcgCore.Controller.Cards;
using System.Collections.Generic;

namespace CcgCore.Model
{
    public class CardEffectActivationContext
    {
        public bool wasActionCancelled;

        public List<Card> selectedCards;

        public Card activatedCard;

        public Card targetCard;
        public CardStack targetStack;

        public CardGameControllerBase cardGameController;

        public ActorScope triggerActor;

        public List<CommonEffect.ParameterisedCalculationFactor> parameters;
    }
}
