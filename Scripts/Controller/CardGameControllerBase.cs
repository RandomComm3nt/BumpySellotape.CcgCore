using CcgCore.Controller.Actors;
using CcgCore.Controller.Cards;
using CcgCore.Controller.Events;
using CcgCore.Model.Config;
using CcgCore.Model.Parameters;

namespace CcgCore.Controller
{
    public class CardGameControllerBase : ParameterScope
    {
        protected EventOrchestratorBase eventOrchestrator;

        public CardGameConfig CardGameConfig { get; }
        public CardFactory CardFactory { get; }
        public TurnSystem TurnSystem { get; protected set; }

        public CardGameControllerBase(CardGameConfig config) 
            : base(ParameterScopeLevel.Game, null)
        {
            eventOrchestrator = new EventOrchestratorBase();
            CardGameConfig = config;
            CardFactory = new CardFactory();
        }

        public override void RaiseEvent(CardGameEvent cardGameEvent)
        {
            base.RaiseEvent(cardGameEvent);
            eventOrchestrator.RaiseEvent(cardGameEvent, this);
        }
    }
}
