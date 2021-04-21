using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using Stats.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class ChangeStatEffect : TargetedCardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] private StatType statType;

        [SerializeField, FoldoutGroup("@DisplayLabel"), ListDrawerSettings(CustomAddFunction = "GetDefaultFactor")] private List<CalculationFactor> additiveFactors = new List<CalculationFactor>();
        [SerializeField, FoldoutGroup("@DisplayLabel"), ListDrawerSettings(CustomAddFunction = "GetDefaultFactor")] private List<CalculationFactor> multiplicativeFactors = new List<CalculationFactor>();

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope)
        {
            var targets = GetTargetActors(context, thisScope);
            foreach (var actor in targets)
            {
                if (actor.StatCollection.GetStat(statType, out var stat))
                {
                    float value = additiveFactors.Sum(f => f.GetValue(context, actor, 0f)) 
                        * multiplicativeFactors.Select(f => f.GetValue(context, actor, 1f)).Aggregate(1f, (a, b) => a * b);
                    stat.ChangeValue(value);
                }
            }
        }

        private CalculationFactor GetDefaultFactor => new CalculationFactor();

        public override string DisplayLabel => $"Change {(statType ? statType.DisplayName : "[stat]")} by [value]";

        public List<string> GetParameters()
        {
            return additiveFactors.Union(multiplicativeFactors).Where(f => f.IsParamaterised).Select(f => f.ParameterName).ToList();
        }
    }
}