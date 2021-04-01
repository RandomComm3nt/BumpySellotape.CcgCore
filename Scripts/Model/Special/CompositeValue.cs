using CcgCore.Controller.Cards;
using CcgCore.Model.Cards;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CcgCore.Model.Special
{
    [CreateAssetMenu(menuName = "Card/Composite Value")]
    public class CompositeValue : ScriptableObject
    {
        [Serializable]
        private class Factor
        {
            public CardDefinition cardDefinition = null;
            public bool useCounters = false;
            [InlineEditor] public FloatParameter weighting = null;

            public float GetValue(FieldRegion fieldRegion)
            {
                var cards = fieldRegion.FindCards(cardDefinition: cardDefinition);
                var weight = weighting.Evaluate(fieldRegion);
                return (useCounters ? cards.Sum(c => c.Counters) : cards.Count) * weight;
            }
        }

        [SerializeField, ListDrawerSettings(CustomAddFunction = "AddFactor")] private List<Factor> factors = new List<Factor>();

        public float GetValue(FieldRegion fieldRegion)
        {
            return factors.Sum(f => f.GetValue(fieldRegion));
        }

#if UNITY_EDITOR
        private Factor AddFactor(List<Factor> values)
        {
            var valueFloat = CreateInstance<FloatParameter>();
            AssetDatabase.CreateAsset(valueFloat, AssetDatabase.GetAssetPath(this).Replace(".asset", $"_{values.Count}_Factor.asset"));

            return new Factor()
            {
                weighting = valueFloat,
            };
        }

        [Button]
        private void UpdateNames()
        {
            foreach (Factor f in factors)
            {
                var p = AssetDatabase.GetAssetPath(f.weighting);
                var s = AssetDatabase.RenameAsset(p, $"{name}_{f.cardDefinition.name}_Factor");
                if (!string.IsNullOrEmpty(s))
                    Debug.LogWarning(s);
            }
        }
#endif
    }
}
