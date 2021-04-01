namespace CcgCore.Model.Effects
{
    public class CancelEventEffect : CardEffect
    {
        public override void ActivateEffects(CardEffectActivationContextBase context)
        {
            context.wasActionCancelled = true;
        }
    }
}
