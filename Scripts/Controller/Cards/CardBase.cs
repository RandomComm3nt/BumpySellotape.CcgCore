using CcgCore.Controller.Events;
using CcgCore.Model;
using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Controller.Cards
{
    public abstract class CardBase : ParameterScope
    {
        public delegate void CardChanged();
        public event CardChanged OnCardChanged;

        public abstract CardDefinitionBase CardDefinitionBase { get; }
        public int Counters { get; private set; }

        public CardBase(ParameterScope parent)
            : base(ParameterScopeLevel.Card, parent)
        { }

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
    }

    public class CardBase<T> : CardBase
        where T : CardDefinitionBase
    {
        public T CardDefinition { get; private set; }
        override public CardDefinitionBase CardDefinitionBase => CardDefinition;

        public CardBase(T cardDefinition, ParameterScope parent)
            : base(parent)
        {
            CardDefinition = cardDefinition;
        }
    }
}
