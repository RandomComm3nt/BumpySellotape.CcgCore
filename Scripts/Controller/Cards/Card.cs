using BumpySellotape.CcgCore.Controller.Cards.States;
using BumpySellotape.Core.Utilities;
using BumpySellotape.Events.Model.Effects;
using CcgCore.Controller.Events;
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

        public delegate void StateChangedHandler();
        public event StateChangedHandler StateChanged;
        /// <summary>
        /// Invoked when a value within the current state changes
        /// </summary>
        public event SimpleEventHandler StateValueChanged;
        private CardState cardState;
        public CardState CardState
        {
            get => cardState;
            set
            {
                cardState = value;
                StateChanged?.Invoke();
            }
        }

        internal Card(CardDefinition cardDefinition, ParameterScope parent)
            : base(ParameterScopeLevel.Card, parent)
        {
            CardDefinition = cardDefinition;
        }

        public void Remove()
        {
            (GetHigherScope(ParameterScopeLevel.Region) as FieldRegion).RemoveCard(this);
        }

        public void ChangeCounters(int delta)
        {
            Counters += delta;
            if (delta > 0)
                RaiseEvent(new CardGameEvent(Events.EventType.CountersAddedToCard));
            else if (delta < 0)
                RaiseEvent(new CardGameEvent(Events.EventType.CountersRemovedFromCard));
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

        public void AttemptPlayCard(CardStack targetStack, ProcessingContext context)
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
            
            */

            

            if (success)
            {
                var cardEvent = new CardGameEvent(Events.EventType.CardActivationAttempt);
                RaiseEvent(cardEvent);
                if (cardEvent.IsCancelled)
                    FailToPlayCard(context);
                else
                    PlayCard(context);
            }
            else
            {
                FailToPlayCard(context);
            }
        }

        public void PlayCard(ProcessingContext context)
        {
            if (CardDefinition.DebugCard)
            {
                Debug.Log("Playing card " + CardDefinition.name);
                context.logDebugMessages = true;
            }
            var cardEvent = new CardGameEvent(Events.EventType.CardActivationSuccess);
            RaiseEvent(cardEvent);
            if (cardEvent.IsCancelled)
                return;

            CardDefinition.Modules.ForEach(m => m.ActivateCard(context, this));
        }

        public void FailToPlayCard(ProcessingContext context)
        {
            var cardEvent = new CardGameEvent(Events.EventType.CardActivationFailure);
            RaiseEvent(cardEvent);
        }

        public void InvokeStateValueChanged(CardState cardState)
        {
            if (cardState == CardState)
                StateValueChanged?.Invoke();
        }
    }
}
