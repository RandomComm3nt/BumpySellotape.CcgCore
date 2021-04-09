using CcgCore.Controller.Cards;
using CcgCore.Model.Effects.Conditions;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class RemoveCardsEffect : TargetedCardEffect
    {
        [SerializeField, HideLabel, HideReferenceObjectPicker, FoldoutGroup("@DisplayLabel")] private CardCondition cardCondition = new CardCondition();
        //TODO features for first/all/random/choose
        public override void ActivateEffects(CardEffectActivationContext context, Card thisCard)
        {
            var targets = GetTargetActors(context, thisCard);
            foreach (var actor in targets)
            {
                var cards = actor.ActorScope.GetAllChildScopesAtLevel(Parameters.ParameterScopeLevel.Card).ToList();
                foreach (var scope in cards)
                {
                    var card = scope as Card;
                    if (cardCondition.CheckCondition(card))
                        (card.GetHigherScope(Parameters.ParameterScopeLevel.Region) as FieldRegion).RemoveCard(card);
                }
            }
        }

        public override string DisplayLabel => /*cards.Count == 1 && cards[0] ? $"Add card {cards[0].name} to {TargetString}" : */ $"Remove cards from {TargetString}";
    }
}
