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

        public CardDefinition CardDefinitionBase => CardDefinition;
        public int Counters { get; private set; }

        public void ChangeCounters(int delta)
        {
            Counters += delta;
            Debug.Log("counters changed, new total = " + Counters);
            OnCardChanged?.Invoke();
        }

        protected override List<(ParameterScope scope, TriggeredEffect effect)> GetTriggeredEffectsForEvent(CardGameEvent cardGameEvent)
        {
            return CardDefinitionBase.TriggeredEffects
                .Where(e => e.CheckConditions(cardGameEvent, this))
                .Select(e => ((ParameterScope)this, e))
                .ToList();
        }


        public CardDefinition CardDefinition { get; private set; }

        internal Card(CardDefinition cardDefinition, ParameterScope parent)
            : base(ParameterScopeLevel.Card, parent)
        {
            CardDefinition = cardDefinition;
        }



        public string GetDescription()
        {
            var description = "";

            //GetCommonEffects().ForEach(ce => description += ce.Label);

            return description;
        }

        public void AttemptPlayCard(CardStack targetStack)
        {/*
            Debug.Log("Attempt to play card");
            bool success = true;
            foreach (var threshold in CardDefinition.GetModule<MindHackModule>().SuccessThresholds)
            {
                var modifier = targetStack.BaseCard.CardDefinition.ThresholdModifiers.FirstOrDefault(m => m.CompositeValue == threshold.CompositeValue);

                if (!threshold.TestAboveThreshold(CardGameController.cardGameController.mindRegion, targetStack, modifier))
                {
                    success = false;
                    break;
                }
            }

            var context = new CardEffectActivationContext()
            {
                targetStack = targetStack,
                cardGameController = CardGameController.cardGameController,
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
            */
        }

        public void PlayCard(CardEffectActivationContext context)
        {
            /*
            Debug.Log("Play card");
            // TECH DEBT
            if (CardDefinition.GetModule<MindHackModule>().OneOffAction)
            {
                CardDefinition.ActivationEffects.ForEach(e =>
                {
                    if (!context.wasActionCancelled)
                        e.ActivateEffects(context);
                });
            }
            */
        }

        public void FailToPlayCard(CardEffectActivationContext context)
        {

        }
    }
}
