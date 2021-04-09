using CcgCore.Controller.Cards;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class ChangeCountersCardEffect : CardEffect
    {
        [SerializeField] private int value = 1;

        public override void ActivateEffects(CardEffectActivationContext context, Card thisCard)
        {
            context.activatedCard.ChangeCounters(value);
        }
    }
}
