using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CcgCore.Model.Effects.Conditions
{
    [CreateAssetMenu(menuName = "Card/Common Condition")]
    public class CommonConditionDefinition : SerializedScriptableObject
    {
        [field: OdinSerialize] public TriggerCondition TriggerCondition { get; } = new TriggerCondition();
    }
}
