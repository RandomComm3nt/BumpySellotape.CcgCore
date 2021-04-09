namespace CcgCore.Model.Effects
{
    public abstract class CardEffect
    {
        public abstract void ActivateEffects(CardEffectActivationContext context);

        public virtual string DisplayLabel => GetType().Name;
    }
}
