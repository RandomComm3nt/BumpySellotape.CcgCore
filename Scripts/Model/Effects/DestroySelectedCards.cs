using System;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [Serializable]
    public class DestroySelectedCards : CardEffect
    {
        [SerializeField] private bool destroyCopies = false;
        [SerializeField] private Scope scope = Scope.TargetStack;

        public override void ActivateEffects(CardEffectActivationContextBase context)
        {
            if (destroyCopies)
            {
                var cardsToDestroy = context.selectedCards.Select(s => s.CardDefinitionBase).ToList();
                if (scope == Scope.TargetStack)
                {
                    foreach (var c in context.targetStack.StackedCards.Where(tc => cardsToDestroy.Contains(tc.CardDefinition)))
                        context.targetStack.RemoveCard(c);
                }
                else
                    throw new NotImplementedException();
            }
            else
                throw new NotImplementedException();
        }

        public enum Scope
        {
            Game = 0,
            Field = 2,
            Region = 4,
            TargetStack = 6,
        }
    }
}
