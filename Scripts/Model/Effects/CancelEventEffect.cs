using CcgCore.Controller.Cards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class CancelEventEffect : CardEffect
    {
        [SerializeField, HideLabel, ReadOnly] private string cancel = "Cancel Event";

        public override void ActivateEffects(CardEffectActivationContext context, Card thisCard)
        {
            context.wasActionCancelled = true;
        }
    }
}
