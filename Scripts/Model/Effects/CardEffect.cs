using CcgCore.Controller.Cards;

namespace CcgCore.Model.Effects
{
    public abstract class CardEffect
    {
        public abstract void ActivateEffects(CardEffectActivationContextBase context, CardBase thisCard);

        protected virtual string DisplayName => GetType().Name;
    }
}
