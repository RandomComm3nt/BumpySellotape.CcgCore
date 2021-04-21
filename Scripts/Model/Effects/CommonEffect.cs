using CcgCore.Model;
using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BumpySellotape.CcgCore.Model.Effects
{
    public class CommonEffect : CardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), OnValueChanged("EffectDefinitionChanged")] private CommonEffectDefinition effectDefinition;
        [SerializeField, FoldoutGroup("@DisplayLabel"), ListDrawerSettings(CustomAddFunction = "DefaultFactor"), ShowIf("effectDefinition")] private List<ParameterisedCalculationFactor> parameters = new List<ParameterisedCalculationFactor>();

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisCard)
        {
            effectDefinition.ActivateEffect(context, thisCard, parameters);
        }

        public override string DisplayLabel => effectDefinition ? effectDefinition.name : base.DisplayLabel;

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

        public class ParameterisedCalculationFactor : CalculationFactor
        {
            [HideInInspector, NonSerialized] public List<string> parameters;
            [ValueDropdown("parameters"), PropertyOrder(-1), HideLabel(), SuffixLabel(":"), HorizontalGroup("HGroup")] public string key;
        }
    }
}
