using CcgCore.Controller.Cards;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model
{
    [Serializable]
    public class CardFilter
    {
        [ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")]
        [SerializeField] private List<int> tags = null;
        [SerializeField, ShowIf("@tags.Count > 1")] private bool requireAllTags = false;

        public bool TestCard<TCard>(TCard card)
            where TCard : CardBase
        {
            return tags.Count == 0 || (requireAllTags ? tags.All(t => card.CardDefinition.Tags.Contains(t)) : tags.Any(t => card.CardDefinition.Tags.Contains(t)));
        }
    }
}
