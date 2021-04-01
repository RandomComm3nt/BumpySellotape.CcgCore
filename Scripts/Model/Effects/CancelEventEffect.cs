namespace CcgCore.Model.Effects
{
    public class CancelEventEffect : CardEffect
    {
        public override void ActivateEffects(CardEffectActivationContext context)
        {
            context.wasActionCancelled = true;
        }
    }
}
