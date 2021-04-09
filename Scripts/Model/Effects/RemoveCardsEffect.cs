using CcgCore.Controller.Cards;
using CcgCore.Model.Effects.Conditions;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class RemoveCardsEffect : TargetedCardEffect
    {
        [SerializeField, HideLabel, HideReferenceObjectPicker] private CardCondition cardCondition = new CardCondition();
        //TODO features for first/all/random/choose
        public override void ActivateEffects(CardEffectActivationContext context)
        {
            var targets = GetTargetActors(context);
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
    }
}
