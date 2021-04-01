using CcgCore.Controller.Cards;
using CcgCore.Model.Cards;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model.Effects.Conditions
{
    public class CardCondition : Condition<CardBase>
    {
        [SerializeField] private bool checkCardCondition = false;
        [SerializeField, ShowIf("checkCardCondition")] private List<CardDefinition> cardDefinitions = new List<CardDefinition>();

        [SerializeField] private bool checkCounters = false;
        [SerializeField, ShowIf("checkCounters")]
        private ComparisonOperator counterOperator = ComparisonOperator.Equals;
        [SerializeField, ShowIf("checkCounters")] private int counterValue = 0;

        protected override bool CheckConditionInternal(CardBase card)
        {
            return
                (!checkCardCondition || cardDefinitions.Contains(card.CardDefinitionBase)) &&
                (!checkCounters || ComparisonUtility.CompareValue(card.Counters, counterOperator, counterValue))
            ;
        }
    }
}
