using CcgCore.Controller.Cards;

namespace CcgCore.Model.Effects
{
    public abstract class CardEffect
    {
        public abstract void ActivateEffects(CardEffectActivationContext context, Card thisCard);

        public virtual string DisplayLabel => GetType().Name;
    }
}
