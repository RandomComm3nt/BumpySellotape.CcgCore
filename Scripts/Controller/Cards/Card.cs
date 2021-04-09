using CcgCore.Controller.Events;
using CcgCore.Model;
using CcgCore.Model.Cards;
using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Controller.Cards
{
    public class Card : ParameterScope
    {
        public delegate void CardChanged();
        public event CardChanged OnCardChanged;

        public CardDefinition CardDefinition { get; private set; }
        public int Counters { get; private set; }

        internal Card(CardDefinition cardDefinition, ParameterScope parent)
            : base(ParameterScopeLevel.Card, parent)
        {
            CardDefinition = cardDefinition;
        }

        public void ChangeCounters(int delta)
        {
            Counters += delta;
            Debug.Log("counters changed, new total = " + Counters);
            OnCardChanged?.Invoke();
        }
        
        protected override List<(ParameterScope scope, TriggeredEffect effect)> GetTriggeredEffectsForEvent(CardGameEvent cardGameEvent)
        {
            var m = CardDefinition.GetModule<CardPassiveEffects>();
            if (m == null)
                return new List<(ParameterScope scope, TriggeredEffect effect)>();
            return m.TriggeredEffects
                .Where(e => e.CheckConditions(cardGameEvent, this, CardDefinition.DebugCard ? CardDefinition.name : null))
                .Select(e => ((ParameterScope)this, e))
                .ToList();
        }
        
        public string GetDescription()
        {
            var description = "";

            //GetCommonEffects().ForEach(ce => description += ce.Label);

            return description;
        }

        public void AttemptPlayCard(CardStack targetStack)
        {
            bool success = true;
            /*
            foreach (var threshold in CardDefinition.GetModule<MindHackModule>().SuccessThresholds)
            {
                var modifier = targetStack.BaseCard.CardDefinition.ThresholdModifiers.FirstOrDefault(m => m.CompositeValue == threshold.CompositeValue);

                if (!threshold.TestAboveThreshold(CardGameController.cardGameController.mindRegion, targetStack, modifier))
                {
                    success = false;
                    break;
                }
            }
            */

            var context = new CardEffectActivationContext()
            {
                targetStack = targetStack,
                cardGameController = RootScope as CardGameControllerBase,
                activatedCard = this,
            };

            if (success)
            {
                PlayCard(context);
            }
            else
            {
                FailToPlayCard(context);
            }
        }

        public void PlayCard(CardEffectActivationContext context)
        {
            // TECH DEBT - wrong event
            var cardEvent = new CardEvent(CardEvent.CardEventType.CardAdded);
            RaiseEvent(cardEvent);
            if (cardEvent.IsCancelled)
                return;

            var activationEffects = CardDefinition.GetModule<CardActivationEffects>();
            if (activationEffects == null)
                return;

            activationEffects.ActivationEffects.ForEach(e =>
            {
                if (!context.wasActionCancelled)
                    e.ActivateEffects(context);
            });

            if (activationEffects.DestroyWhenPlayed)
            {
                ParentScope?.RemoveChild(this);
            }
        }

        public void FailToPlayCard(CardEffectActivationContext context)
        {

        }
    }
}
