using CcgCore.Model.Parameters;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public abstract class CardEffect
    {
        public abstract void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope);

        public virtual string DisplayLabel => GetType().Name;

        [HideInInspector] public bool allowParameters = false;
    }
}
