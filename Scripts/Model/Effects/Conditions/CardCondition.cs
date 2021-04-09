using CcgCore.Controller;
using CcgCore.Controller.Cards;
using CcgCore.Model.Cards;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects.Conditions
{
    public class CardCondition : Condition<Card>
    {
        [SerializeField] private bool checkCard = false;
        [SerializeField, ShowIf("checkCard"), ValueDropdown("@CcgCore.Controller.CardGameEditor.GetAllCards")] private List<CardDefinition> cardDefinitions = new List<CardDefinition>();
        [SerializeField] private bool checkCardTags = false;
        [SerializeField, ShowIf("checkCardTags"), ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")] private List<int> tags = new List<int>();

        [SerializeField] private bool checkCounters = false;
        [SerializeField, ShowIf("checkCounters")]
        private ComparisonOperator counterOperator = ComparisonOperator.Equals;
        [SerializeField, ShowIf("checkCounters")] private int counterValue = 0;

        protected override bool CheckConditionInternal(Card card)
        {
            return 
                (!checkCard || cardDefinitions.Contains(card.CardDefinition)) &&
                (!checkCounters || ComparisonUtility.CompareValue(card.Counters, counterOperator, counterValue)) &&
                (!checkCardTags || card.CardDefinition.Tags.Intersect(tags).Count() > 0)
            ;
        }

#if UNITY_EDITOR
        public string DisplayName
        {
            get
            {
                if (checkCard && cardDefinitions.Count == 1 && cardDefinitions[0])
                    return cardDefinitions[0].name;
                if (checkCardTags && tags.Count == 1)
                    return "[" + CardGameEditor.CardGameConfig.GetTagName(tags[0]) + "]";
                return "";
            }
        }
    }
#endif
}
