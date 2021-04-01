namespace CcgCore.Model.Effects
{
    public abstract class CardEffect
    {
        public abstract void ActivateEffects(CardEffectActivationContextBase context);

        protected virtual string DisplayName => GetType().Name;
    }
}
