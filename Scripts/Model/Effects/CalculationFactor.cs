using BumpySellotape.Core.Model.Effects;
using BumpySellotape.Core.Stats.Model;
using BumpySellotape.TurnBased.Controller.Actors;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [HideReferenceObjectPicker]
    public class CcgCalculationFactor : CalculationFactor
    {
        /*
        public override float GetValue(CardEffectActivationContext context, Actor targetActor, float defaultValue)
        {
            return factorType switch
            {
                FactorType.FixedValue => value,
                FactorType.ValueRange => Random.Range(valueRange.x, valueRange.y),
                FactorType.Stat => GetStatValue(context.triggerActor.Actor, targetActor, defaultValue),
                FactorType.Parameter => context.parameters.FirstOrDefault(p => p.key == parameterName)?.GetValue(context, targetActor, defaultValue) ?? defaultValue,
                _ => base.GetValue(context, targetActor, defaultValue),
            };
        }
        */
    }
}