using CcgCore.Controller.Cards;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [Serializable]
    public class SelectCardFromStack : CardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] private CardFilter cardFilter = null;
        [SerializeField, FoldoutGroup("@DisplayLabel")] private StackCardSelectionType stackCardSelectionType = StackCardSelectionType.Bottom;

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope)
        {
            var targets = GetValidTargets(context);
            Card target = null;
            switch (stackCardSelectionType)
            {
                case StackCardSelectionType.Bottom:
                    target = targets[0];
                    break;
                case StackCardSelectionType.Top:
                    target = targets[targets.Count - 1];
                    break;
                case StackCardSelectionType.Random:
                    target = targets[UnityEngine.Random.Range(0, targets.Count)];
                    break;
                case StackCardSelectionType.UserSelection:
                case StackCardSelectionType.TargetSelection:
                    throw new NotImplementedException(); // TECH DEBT
            }

            context.selectedCards = new List<Card>() { target };
        }

        private List<Card> GetValidTargets(CardEffectActivationContext context)
        {
            var unprotectedCards = new List<Card>();
            bool stopAdding = true;
            for (int i = 0; i < context.targetStack.StackedCards.Count; i++)
            {
                //var spe = context.targetStack.StackedCards[i].CardDefinition.StackProtectionEffect;
                if (!stopAdding)
                    unprotectedCards.Add(context.targetStack.StackedCards[i]);
                /*
                if (spe.HasEffect && spe.ActionsToProtectFrom.TestCard<Card>(context.activatedCard))
                {
                    switch (spe.Direction)
                    {
                        case StackProtectionEffect.ProtectionDirection.FullStack:
                            return new List<Card>();
                        case StackProtectionEffect.ProtectionDirection.Below:
                            unprotectedCards = new List<Card>() { context.targetStack.StackedCards[i] }; // cards below are protected
                            break;
                        case StackProtectionEffect.ProtectionDirection.Above:
                            stopAdding = true;
                            break;
                    }
                }
                */
            }

            var cardsToChooseFrom = unprotectedCards
                .Where(c => cardFilter == null || cardFilter.TestCard<Card>(c))
                .ToList();
            return cardsToChooseFrom;
        }

        // TECH DEBT - not yet run
        public bool CanActivate(CardEffectActivationContext context)
        {
            return GetValidTargets(context).Count > 0;
        }

        public enum StackCardSelectionType
        {
            Top = 0,
            Bottom,
            Random,
            UserSelection,
            TargetSelection,
        }
    }
}
