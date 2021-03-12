using System;
using UnityEngine;

namespace CcgCore.Model.Parameters
{
    [Serializable]
    public abstract class ParameterModifier<T>
    {
        [field: SerializeField] public Parameter<T> DynamicValue { get; private set; }
        [field: SerializeField] public T AdditiveModifier { get; private set; }
        [field: SerializeField] public float MultiplicativeModifier { get; private set; } = 1f;

        public abstract T ModifyValue(T value);
    }

    [Serializable]
    public class IntParameterModifier : ParameterModifier<int>
    {
        public override int ModifyValue(int value)
        {
            return Mathf.RoundToInt(value * MultiplicativeModifier) + AdditiveModifier;
        }
    }

    [Serializable]
    public class FloatParameterModifier : ParameterModifier<float>
    {
        public override float ModifyValue(float value)
        {
            return (value * MultiplicativeModifier) + AdditiveModifier;
        }
    }
}
