using BumpySellotape.Events.Model.Conditions;
using BumpySellotape.Core.Stats.Model;
using CcgCore.Controller.Actors;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace BumpySellotape.CcgCore.CcgCore.Model.Effects.Conditions
{
    public class StatCondition : BumpySellotape.CcgCore.Model.Effects.Conditions.Condition
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] private StatType statType;
        [SerializeField, FoldoutGroup("@DisplayLabel")] private ComparisonOperator comparisonOperator;
        [SerializeField, FoldoutGroup("@DisplayLabel")] private float value;

        public override bool CheckCondition(ParameterScope scope)
        {
            if (scope is ActorScope a)
            {
                var statValue = a.Actor.StatCollection.GetStatValue(statType);
                return ComparisonUtility.CompareValue(statValue, comparisonOperator, value);
            }
            throw new NotImplementedException();
        }

        public override string DisplayLabel => $"{statType.DisplayName} {ComparisonUtility.GetDisplayString(comparisonOperator)} {value}";
    }
}
