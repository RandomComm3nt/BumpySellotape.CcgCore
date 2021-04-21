using BumpySellotape.CcgCore.CcgCore.Model.Effects;
using BumpySellotape.CcgCore.Model.Effects;
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
        [OdinSerialize, HideReferenceObjectPicker, OnValueChanged("AllowParameters")] private List<CardEffect> cardEffects = new List<CardEffect>();

        public void ActivateEffect(CardEffectActivationContext context, ParameterScope thisCard, List<CommonEffect.ParameterisedCalculationFactor> parameters)
        {
            context.parameters = parameters;
            foreach (var ce in cardEffects)
            {
                ce.ActivateEffects(context, thisCard);
            }
            context.parameters = null;
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

        public List<string> GetParameters()
        {
            return cardEffects.Where(ce => ce is ChangeStatEffect).SelectMany(ce => (ce as ChangeStatEffect).GetParameters()).ToList();
        }
#endif
    }
}
