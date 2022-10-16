using BumpySellotape.Core.Events.Model.Effects;
using BumpySellotape.Events.Model.Effects;
using CcgCore.Model;
using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BumpySellotape.CcgCore.Model.Effects
{
    public partial class CommonEffect : CardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), OnValueChanged("EffectDefinitionChanged")] private CommonEffectDefinition effectDefinition;
        [SerializeField, FoldoutGroup("@DisplayLabel"), ListDrawerSettings(CustomAddFunction = "DefaultFactor"), ShowIf(nameof(ShowParameters))] private List<ParameterisedCalculationFactor> parameters = new();

        public CommonEffectDefinition EffectDefinition { get => effectDefinition; set => effectDefinition = value; }

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisCard)
        {
            //effectDefinition.ActivateEffect(context, thisCard, parameters);
        }

        public override void Process(ProcessingContext processingContext)
        {
            effectDefinition.Process(processingContext, parameters);
        }

        public override string DisplayLabel => effectDefinition ? GetDescription() : base.DisplayLabel;

        private ParameterisedCalculationFactor DefaultFactor()
        {
            return new ParameterisedCalculationFactor()
            {
                parameters = effectDefinition.GetParameters()
            };
        }

        private void EffectDefinitionChanged()
        {
            parameters.Clear();
            if (!effectDefinition)
                return;
            var eParams = effectDefinition.GetParameters();
            foreach(var p in eParams)
            {
                parameters.Add(new ParameterisedCalculationFactor()
                {
                    parameters = eParams,
                    key = p
                });
            }
        }

        public string GetDescription()
        {
            var dict = parameters.ToDictionary(p => p.key, p => p.DisplayValue);
            return effectDefinition.GetDescriptionWithSubstitutions(dict);
        }

        #region UNITY_EDITOR

        private bool ShowParameters => effectDefinition != null && effectDefinition.IsParametrised;

        #endregion
    }
}
