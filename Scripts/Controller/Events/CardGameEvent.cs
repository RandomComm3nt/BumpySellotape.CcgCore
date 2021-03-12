using CcgCore.Model.Parameters;
using System.Collections.Generic;

namespace CcgCore.Controller.Events
{
    public class CardGameEvent
    {
        public List<ParameterScope> callingHeirachy;

        public string EventType { get; }
        public bool IsCancelled { get; private set;  }
        public override string ToString() => $"EventType: {EventType}";

        public CardGameEvent(string eventType)
        {
            EventType = eventType;
            callingHeirachy = new List<ParameterScope>();
        }

        public ParameterScope GetFromHeirachyAtLevel(ParameterScopeLevel parameterScopeLevel)
        {
            return callingHeirachy.Find(s => s.ScopeLevel == parameterScopeLevel);
        }

        public void CancelEvent()
        {
            IsCancelled = true;
        }
    }
}
