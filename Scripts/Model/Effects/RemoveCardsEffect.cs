using CcgCore.Controller.Cards;
using CcgCore.Model.Effects.Conditions;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class RemoveCardsEffect : TargetedCardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), PropertyOrder(-1)] private bool useSelectedCards;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideIf("HideTargettingFields"), HideLabel, HideReferenceObjectPicker] private CardCondition cardCondition = new CardCondition();
        //TODO features for first/all/random/choose

        protected override bool HideTargettingFields => useSelectedCards;

        public override void ActivateEffects(CardEffectActivationContext context, Card thisCard)
        {
            if (useSelectedCards)
            {
                context.selectedCards.ForEach(c => c.Remove());
                return;
            }
            var targets = GetTargetActors(context, thisCard);
            foreach (var actor in targets)
            {
                var cards = actor.ActorScope.GetAllChildScopesAtLevel(Parameters.ParameterScopeLevel.Card).ToList();
                foreach (var scope in cards)
                {
                    var card = scope as Card;
                    if (cardCondition.CheckCondition(card))
                        card.Remove();
                }
            }
        }

        public override string DisplayLabel
        {
            get
            {
                if (useSelectedCards)
                    return "Remove selected cards";
                /*cards.Count == 1 && cards[0] ? $"Add card {cards[0].name} to {TargetString}" : */
                return $"Remove cards from {TargetString}";
            }
        }
    }
}
