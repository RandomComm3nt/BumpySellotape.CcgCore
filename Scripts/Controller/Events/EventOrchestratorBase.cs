using CcgCore.Model;
using CcgCore.Model.Parameters;
using UnityEngine;

namespace CcgCore.Controller.Events
{
    public class EventOrchestratorBase
    {
        public void RaiseEvent(CardGameEvent cardGameEvent, ParameterScope topLevelScope)
        {
            Debug.Log("Processing event: " + cardGameEvent);
            var effects = topLevelScope.GetAllTriggeredEffectsForEvent(cardGameEvent);
            var context = CreateContextFromEvent(cardGameEvent);
            Debug.Log($"{effects.Count} effects triggered");

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
        }

        protected virtual CardEffectActivationContext CreateContextFromEvent(CardGameEvent cardGameEvent)
        {
            return new CardEffectActivationContext()
            {

            };
        }
    }
}
