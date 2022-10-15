using CcgCore.Controller.Actors;
using CcgCore.Controller.Cards;
using CcgCore.Model;
using CcgCore.Model.Parameters;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Controller.Events
{
    public class EventOrchestratorBase
    {
        private readonly List<CardGameEvent> queuedEvents = new List<CardGameEvent>();

        public void RaiseEvent(CardGameEvent cardGameEvent, ParameterScope topLevelScope)
        {
            if (queuedEvents.Count > 10)
            {
                Debug.LogWarning("More than 10 events queued! Rejecting further events");
                return;
            }
            queuedEvents.Add(cardGameEvent);
            if (queuedEvents.Count == 1)
                ProcessEvent(cardGameEvent, topLevelScope);
        }

        private void ProcessEvent(CardGameEvent cardGameEvent, ParameterScope topLevelScope)
        {
            var effects = topLevelScope.GetAllTriggeredEffectsForEvent(cardGameEvent);
            var context = CreateContextFromEvent(cardGameEvent);

            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].effect.ActivateEffect(effects[i].scope, context);
                if (context.wasActionCancelled)
                {
                    // RaiseEvent() - raise cancellation event
                    cardGameEvent.CancelEvent();
                    break;
                }
            }
            queuedEvents.Remove(cardGameEvent);
            if (queuedEvents.Count > 0)
                ProcessEvent(queuedEvents[0], topLevelScope);
        }

        protected virtual CardEffectActivationContext CreateContextFromEvent(CardGameEvent cardGameEvent)
        {
            return new CardEffectActivationContext(null)
            {
                activatedCard = cardGameEvent.callingHeirachy[0] as Card,
                triggerActor = (cardGameEvent.GetFromHeirachyAtLevel(ParameterScopeLevel.Actor) as ActorScope),
                cardGameController = cardGameEvent.GetFromHeirachyAtLevel(ParameterScopeLevel.Game) as CardGameControllerBase,
            };
        }
    }
}
