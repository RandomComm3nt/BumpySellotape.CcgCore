using CcgCore.Controller.Cards;
using CcgCore.Model.Effects.Conditions;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class RemoveCardsEffect : TargetedCardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), PropertyOrder(-1)] private bool useSelectedCards;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideIf("HideTargettingFields"), HideLabel, HideReferenceObjectPicker] private CardCondition cardCondition = new CardCondition();
        [SerializeField, FoldoutGroup("@DisplayLabel")] private bool logAction = false;
        //TODO features for first/all/random/choose

        protected override bool HideTargettingFields => useSelectedCards;

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope)
        {
            if (useSelectedCards)
            {
                context.selectedCards.ForEach(c => RemoveCard(context, c));
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
                        RemoveCard(context, card);
                }
            }
        }

        private void RemoveCard(CardEffectActivationContext context, Card c)
        {
            if (logAction)
                context.cardGameController.OutputMessage($"[{c.CardDefinition.name} removed]");
            c.Remove();
        }

        public override string DisplayLabel
        {
            get
            {
                string c = "cards";
                if (useSelectedCards)
                    c = "selected cards";
                if (cardCondition.HasNiceDisplayLabel)
                    c = cardCondition.DisplayLabel;
                return $"Remove {c} from {TargetString}";
            }
        }
    }
}
