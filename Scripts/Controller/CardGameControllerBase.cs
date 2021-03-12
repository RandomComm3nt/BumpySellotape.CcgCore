using CcgCore.Controller.Cards;
using CcgCore.Controller.Events;
using CcgCore.Model;
using CcgCore.Model.Parameters;

namespace CcgCore.Controller
{
    public class CardGameControllerBase : ParameterScope
    {
        protected EventOrchestratorBase eventOrchestrator;

        public CardGameControllerBase() 
            : base(ParameterScopeLevel.Game, null)
        {
            eventOrchestrator = new EventOrchestratorBase();
        }

        public override void RaiseEvent(CardGameEvent cardGameEvent)
        {
            base.RaiseEvent(cardGameEvent);
            eventOrchestrator.RaiseEvent(cardGameEvent, this);
        } 
    }
}
