using CcgCore.Controller.Cards;
using System;

namespace CcgCore.Model.Effects
{
    public abstract class DerivedCardEffect<TContext, TCard> : CardEffect
        where TContext : CardEffectActivationContextBase
        where TCard : CardBase
    {
        public override void ActivateEffects(CardEffectActivationContextBase context)
        {
            if (context is TContext t)
                ActivateEffects(t, context.activatedCard as TCard);
            else
                throw new Exception($"DerivedCardEffect expected a context of type {typeof(TContext).Name} but received type {context.GetType().Name}");
        }

        protected abstract void ActivateEffects(TContext context, TCard thisCard);
    }
}
