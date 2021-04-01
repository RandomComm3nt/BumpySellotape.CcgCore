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
    public class CardBase : ParameterScope
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

        public CardBase(CardDefinition cardDefinition, ParameterScope parent)
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

        public virtual void AttemptPlayCard(CardStack targetStack)
        {
            
        }

        public virtual void PlayCard(CardEffectActivationContextBase context)
        {

        }
    }
}
