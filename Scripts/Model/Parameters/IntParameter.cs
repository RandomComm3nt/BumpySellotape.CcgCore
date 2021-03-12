using UnityEngine;

namespace CcgCore.Model.Parameters
{
    [CreateAssetMenu(menuName = "Parameter/Int")]
    public class IntParameter : Parameter<int>
    {
        protected override int EvaluateInner(ParameterScope scope)
        {
            return DefaultValue;
        }
    }
}
