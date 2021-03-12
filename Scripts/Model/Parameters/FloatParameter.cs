using UnityEngine;

namespace CcgCore.Model.Parameters
{
    [CreateAssetMenu(menuName = "Parameter/Float")]
    public class FloatParameter : Parameter<float>
    {
        protected override float EvaluateInner(ParameterScope scope)
        {
            return DefaultValue;
        }
    }
}
