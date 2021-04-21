using CcgCore.Controller.Cards;
using CcgCore.Model.Effects.Conditions;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class ChangeCountersCardEffect : TargetedCardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), PropertyOrder(-1)] private bool useSelectedCards;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideLabel, HideReferenceObjectPicker, HideIf("HideTargettingFields")] private CardCondition cardCondition = new CardCondition();
        [SerializeField, FoldoutGroup("@DisplayLabel")] private int value = 1;

        protected override bool HideTargettingFields => useSelectedCards;

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope)
        {
            if (useSelectedCards)
            {
                context.selectedCards.ForEach(c => c.ChangeCounters(value));
                return;
            }
            var targets = GetTargetActors(context, thisScope);
            foreach (var actor in targets)
            {
                var cards = actor.ActorScope.GetAllChildScopesAtLevel(ParameterScopeLevel.Card).ToList();
                foreach (var scope in cards)
                {
                    var card = scope as Card;
                    if (cardCondition.CheckCondition(card))
                        card.ChangeCounters(value);
                }
            }
        }

        public override string DisplayLabel
        {
            get
            {
                var operation = value >= 0 ? "Add" : "Remove";
                var counterPlural = (value == 1 || value == -1) ? "counter" : "counters"; 
                var fromTo = value >= 0 ? "to" : "from";
                var selection = useSelectedCards ? "selected cards" : cardCondition.DisplayLabel;

                return $"{operation} {Mathf.Abs(value)} {counterPlural} {fromTo} {selection}";
            }
        }
    }
}
