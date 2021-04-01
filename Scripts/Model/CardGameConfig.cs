using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CcgCore.Model
{
    [CreateAssetMenu(menuName = "Card Game Config")]
    public class CardGameConfig : SerializedScriptableObject
    {
        [field: OdinSerialize] public ValueDropdownList<int> CardTags { get; private set; } = new ValueDropdownList<int>();
    }
}
