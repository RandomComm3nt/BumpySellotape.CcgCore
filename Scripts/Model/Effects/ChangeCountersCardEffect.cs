using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class ChangeCountersCardEffect : CardEffect
    {
        [SerializeField] private int value = 1;

        public override void ActivateEffects(CardEffectActivationContextBase context)
        {
            context.activatedCard.ChangeCounters(value);
        }
    }
}
