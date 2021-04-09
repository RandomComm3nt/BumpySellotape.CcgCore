using Sirenix.OdinInspector;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class CancelEventEffect : CardEffect
    {
        [SerializeField, HideLabel, ReadOnly] private string cancel = "Cancel Event";

        public override void ActivateEffects(CardEffectActivationContext context)
        {
            context.wasActionCancelled = true;
        }
    }
}
