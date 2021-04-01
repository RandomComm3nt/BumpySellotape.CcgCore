using CcgCore.Controller.Cards;
using CcgCore.Model.Parameters;
using System;
using UnityEngine;

namespace CcgCore.Model.Special
{
    [Serializable]
    public class CompositeValueThreshold
    {
        [SerializeField] private int thresholdInt = 0;
        [Tooltip("Optional Parameter to allow modification of threshold")]
        [SerializeField] private IntParameter backingParameter = null;

        [field: SerializeField] public CompositeValue CompositeValue {get; private set; }

        public int GetThresholdValue(ParameterScope scope)
        {
            return thresholdInt + (backingParameter != null ? backingParameter.Evaluate(scope) : 0);
        }

        public bool TestAboveThreshold(FieldRegion fieldRegion, ParameterScope scope, CompositeValueThreshold modifier = null)
        {
            return CompositeValue.GetValue(fieldRegion) >= GetThresholdValue(scope) + (modifier?.GetThresholdValue(scope) ?? 0);
        }
    }
}
