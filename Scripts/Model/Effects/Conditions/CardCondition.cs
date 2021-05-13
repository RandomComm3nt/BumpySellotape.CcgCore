using BumpySellotape.Events.Model.Conditions;
using CcgCore.Controller;
using CcgCore.Controller.Cards;
using CcgCore.Model.Cards;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects.Conditions
{
    public class CardCondition : BumpySellotape.CcgCore.Model.Effects.Conditions.Condition<Card>
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] private bool checkCard = false;
        [SerializeField, FoldoutGroup("@DisplayLabel"), ShowIf("checkCard"), ValueDropdown("@CcgCore.Controller.CardGameEditor.GetAllCards")] private List<CardDefinition> cardDefinitions = new List<CardDefinition>();
        [SerializeField, FoldoutGroup("@DisplayLabel")] private bool checkCardTags = false;
        [SerializeField, FoldoutGroup("@DisplayLabel"), ShowIf("checkCardTags"), ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")] private List<int> tags = new List<int>();

        [SerializeField, FoldoutGroup("@DisplayLabel")] private bool checkCounters = false;
        [SerializeField, HorizontalGroup("@DisplayLabel/Counters"), ShowIf("checkCounters"), LabelText("Counters")]
        private ComparisonOperator counterOperator = ComparisonOperator.Equals;
        [SerializeField, HorizontalGroup("@DisplayLabel/Counters"), ShowIf("checkCounters"), HideLabel] private int counterValue = 0;

        protected override bool CheckConditionInternal(Card card)
        {
            return 
                (!checkCard || cardDefinitions.Contains(card.CardDefinition)) &&
                (!checkCounters || ComparisonUtility.CompareValue(card.Counters, counterOperator, counterValue)) &&
                (!checkCardTags || card.CardDefinition.Tags.Intersect(tags).Count() > 0)
            ;
        }

        public bool HasNiceDisplayLabel => (checkCard && cardDefinitions.Count == 1 && cardDefinitions[0]) || (checkCardTags && tags.Count == 1);

#if UNITY_EDITOR
        public override string DisplayLabel
        {
            get
            {
                if (checkCard && cardDefinitions.Count == 1 && cardDefinitions[0])
                    return cardDefinitions[0].name;
                if (checkCardTags && tags.Count == 1)
                    return "[" + CardGameEditor.CardGameConfig.GetTagName(tags[0]) + "]";
                if (checkCounters)
                    return $"Counters {counterOperator} {counterValue}";
                return "[Condition]";
            }
        }
    }
#endif
}
