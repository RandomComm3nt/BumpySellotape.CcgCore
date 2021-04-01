namespace CcgCore.Model.Effects
{
    public abstract class CardEffect
    {
        public abstract void ActivateEffects(CardEffectActivationContext context);

        protected virtual string DisplayName => GetType().Name;
    }
}
