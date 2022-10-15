using BumpySellotape.Events.Model.Effects;
using CcgCore.Model.Parameters;
using System;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public abstract class CardEffect: IEffect
    {
        public abstract void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope);

        public virtual void Process(ProcessingContext processingContext)
        {
            throw new NotImplementedException();
        }

        public virtual string DisplayLabel => GetType().Name;

        public string Label => DisplayLabel;

        [HideInInspector] public bool allowParameters = false;
    }
}
