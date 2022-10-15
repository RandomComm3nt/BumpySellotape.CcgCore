using BumpySellotape.CcgCore.CcgCore.Model.Effects;
using BumpySellotape.Core.Events.Model.Effects;
using BumpySellotape.Events.Model.Effects;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [CreateAssetMenu(menuName = "Card/Common Effect")]
    public class CommonEffectDefinition : SerializedScriptableObject
    {
        [OdinSerialize, HideReferenceObjectPicker, OnValueChanged("AllowParameters")] private List<IEffect> cardEffects = new();
        [field: SerializeField] public string Description { get; private set; }

        public void ActivateEffect(CardEffectActivationContext context, ParameterScope thisCard, List<ParameterisedCalculationFactor> parameters)
        {
            //context.parameters = parameters;
            foreach (var ce in cardEffects)
            {
                //ce.ActivateEffects(context, thisCard);
            }
            context.parameters = null;
        }

        public void Process(ProcessingContext context, List<ParameterisedCalculationFactor> parameters)
        {
            context.parameters = parameters;
            context.effectsToProcess.InsertRange(0, cardEffects.ToList());
        }

        public string GetDescriptionWithSubstitutions(Dictionary<string, string> parameters)
        {
            var d = Description;
            foreach (var p in parameters.Keys)
            {
                d = d.Replace($"{{{p}}}", parameters[p]);
            }
            return d;
        }

#if UNITY_EDITOR
        private void AllowParameters()
        {
            cardEffects.ForEach(ce =>
            {
                var t = ce.GetType();
                t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.FieldType == typeof(ParameterisedFloat))
                    .ForEach(p => {
                        var f = (ParameterisedFloat)p.GetValue(ce);
                        f.allowParameter = true;
                        p.SetValue(ce, f);
                    });
            });
        }

        [ShowInInspector, ShowIf(nameof(IsParametrised))] private string ParameterNames => IsParametrised ? GetParameters().Aggregate((c, n) => c + ", " + n) : "";

        public bool IsParametrised => GetParameters().Count > 0;

        public List<string> GetParameters()
        {
            return cardEffects.SelectMany(ce => ce.GetParameterNames()).ToList();
        }
#endif
    }
}
