using CcgCore.Controller;
using CcgCore.Controller.Cards;
using System.Collections.Generic;

namespace CcgCore.Model
{
    public class CardEffectActivationContextBase
    {
        public bool wasActionCancelled;

        public List<CardBase> selectedCards;

        public CardBase activatedCard;

        public CardBase targetCard;
        public CardStack targetStack;

        public CardGameControllerBase cardGameController;
    }
}
