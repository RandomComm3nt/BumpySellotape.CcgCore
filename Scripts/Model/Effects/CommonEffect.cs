using CcgCore.Controller.Cards;
using CcgCore.Model;
using CcgCore.Model.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BumpySellotape.CcgCore.Model.Effects
{
    public class CommonEffect : CardEffect
    {
        [field: SerializeField, FoldoutGroup("@DisplayLabel")] public CommonEffectDefinition EffectDefinition;

        public override void ActivateEffects(CardEffectActivationContext context, Card thisCard)
        {
            throw new System.NotImplementedException();
        }

        public override string DisplayLabel => base.DisplayLabel;
    }
}
