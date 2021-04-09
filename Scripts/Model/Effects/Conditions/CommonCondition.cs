using Sirenix.OdinInspector;
using UnityEngine;

namespace CcgCore.Model.Effects.Conditions
{
    public class CommonCondition
    {
        [field: SerializeField, HideLabel] public CommonConditionDefinition CommonConditionDefinition { get; private set; }
    }
}
