using BumpySellotape.Stats.Model;
using BumpySellotape.TurnBased.Controller.Actors;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [HideReferenceObjectPicker]
    public class CalculationFactor
    {
        public enum FactorType
        {
            FixedValue,
            ValueRange,
            Stat,
            Parameter
        }

        public enum MultiplierTarget
        {
            This,
            Target,
        }

        [SerializeField, HorizontalGroup("HGroup"), HideLabel] private FactorType factorType;
        [SerializeField, HorizontalGroup("HGroup"), HideLabel, ShowIf("factorType", FactorType.FixedValue)] private float value;
        [SerializeField, HorizontalGroup("HGroup"), HideLabel, ShowIf("factorType", FactorType.ValueRange)] private Vector2 valueRange;
        [SerializeField, HorizontalGroup("HGroup"), HideLabel, ShowIf("factorType", FactorType.Stat)] private MultiplierTarget statOwner;
        [SerializeField, HorizontalGroup("HGroup"), HideLabel, ShowIf("factorType", FactorType.Stat)] private StatType multiplierStatType;
        [SerializeField, HorizontalGroup("HGroup"), HideLabel, ShowIf("factorType", FactorType.Parameter)] private string parameterName;

        public bool IsParamaterised => factorType == FactorType.Parameter;
        public string ParameterName => parameterName;

        public float GetValue(CardEffectActivationContext context, Actor targetActor, float defaultValue)
        {
            return factorType switch
            {
                FactorType.FixedValue => value,
                FactorType.ValueRange => Random.Range(valueRange.x, valueRange.y),
                FactorType.Stat => GetStatValue(context.triggerActor.Actor, targetActor, defaultValue),
                FactorType.Parameter => context.parameters.FirstOrDefault(p => p.key == parameterName)?.GetValue(context, targetActor, defaultValue) ?? defaultValue,
                _ => throw new System.NotImplementedException(),
            };
        }

        private float GetStatValue(Actor thisActor, Actor targetActor, float defaultValue)
        {
            if (!(statOwner == MultiplierTarget.This ? thisActor : targetActor).StatCollection.GetStat(multiplierStatType, out var stat))
                return defaultValue;
            return stat.Value;
        }
    }
}