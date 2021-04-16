using CcgCore.Controller.Cards;
using CcgCore.Model.Effects.Conditions;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class SelectCardsEffect : TargetedCardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), PropertyOrder(-2)] private SelectionType selectionType = SelectionType.ReplaceSelection;
        [SerializeField, FoldoutGroup("@DisplayLabel"), PropertyOrder(-1)] private bool selectThisCard;
        [SerializeField, HideLabel, HideReferenceObjectPicker, FoldoutGroup("@DisplayLabel"), HideIf("selectThisCard")] private CardCondition cardCondition = new CardCondition();

        protected override bool HideTargettingFields => selectThisCard;

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope)
        {
            var cardsToSelect = new List<Card>();
            if (selectThisCard)
                cardsToSelect.Add(thisScope as Card);
            else
            {
                var targets = GetTargetActors(context, thisScope);
                foreach (var actor in targets)
                {
                    var cards = actor.ActorScope.GetAllChildScopesAtLevel(Parameters.ParameterScopeLevel.Card).ToList();
                    foreach (var scope in cards)
                    {
                        var card = scope as Card;
                        if (cardCondition.CheckCondition(card))
                            cardsToSelect.Add(card);
                    }
                }
            }
            
            switch (selectionType)
            {
                case SelectionType.AddToSelection:
                    context.selectedCards.AddRange(cardsToSelect);
                    break;
                case SelectionType.RemoveFromSelection:
                    cardsToSelect.ForEach(c => context.selectedCards.Remove(c));
                    break;
                case SelectionType.ReplaceSelection:
                    context.selectedCards = cardsToSelect;
                    break;
            }
        }

        public override string DisplayLabel
        {
            get
            {
                if (selectThisCard)
                    return "Select this card";
                return $"Select card - {TargetString} - {cardCondition.DisplayLabel}";
            }
        }

        public enum SelectionType
        {
            AddToSelection,
            RemoveFromSelection,
            ReplaceSelection
        }
    }
}
