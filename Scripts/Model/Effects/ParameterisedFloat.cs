using Sirenix.OdinInspector;
using UnityEngine;

namespace BumpySellotape.CcgCore.CcgCore.Model.Effects
{
    [InlineProperty]
    public struct ParameterisedFloat
    {
        [HideInInspector] public string parameterLabel;
        [HideInInspector] public bool allowParameter;
        [ShowIf("allowParameter"), HideLabel, HorizontalGroup("1")] public bool useParameter;
        [HideIf("useParameter"), HideLabel, HorizontalGroup("1")] public float value;

        public ParameterisedFloat(string parameterLabel, float value)
        {
            this.parameterLabel = parameterLabel;
            this.value = value;
            allowParameter = false;
            useParameter = false;
        }

        public override string ToString()
        {
            return useParameter ? "[Parameter]" : value.ToString();
        }
    }
}
