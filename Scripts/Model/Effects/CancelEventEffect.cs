﻿using CcgCore.Controller.Cards;
using System;

namespace CcgCore.Model.Effects
{
    public class CancelEventEffect : CardEffect
    {
        public override void ActivateEffects(CardEffectActivationContextBase context, CardBase thisCard)
        {
            context.wasActionCancelled = true;
        }
    }
}
