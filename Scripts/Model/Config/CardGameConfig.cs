using CcgCore.Controller;
using CcgCore.View;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Config
{
    [CreateAssetMenu(menuName = "Card Game Config")]
    public class CardGameConfig : SerializedScriptableObject
    {
        [field: SerializeField] public string CardPathFilter { get; private set; }
        [field: SerializeField] public CardDisplayBase CardDisplayPrefab { get; private set; }
        [field: OdinSerialize] public ValueDropdownList<int> CardTags { get; } = new ValueDropdownList<int>();
        [field: SerializeField] public List<FieldTemplateScope> FieldScopes { get; private set; }

#if UNITY_EDITOR
        [Button]
        private void SetActive()
        {
            CardGameEditor.CardGameConfig = this;
        }

        public string GetTagName(int value)
        {
            return CardTags.First(testc => testc.Value == value).Text;
        }
#endif
    }
}
