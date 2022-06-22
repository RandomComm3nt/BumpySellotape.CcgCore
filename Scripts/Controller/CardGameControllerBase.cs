using BumpySellotape.TurnBased.Controller.Actors;
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

        public delegate void OutputMessageSent(string message);
        public event OutputMessageSent OnOutputMessageSent;

        public CardGameControllerBase(CardGameConfig config, CardFactory cardFactory) 
            : base(ParameterScopeLevel.Game, null)
        {
            eventOrchestrator = new EventOrchestratorBase();
            CardGameConfig = config;
            CardFactory = cardFactory;
        }

        public override void RaiseEvent(CardGameEvent cardGameEvent)
        {
            base.RaiseEvent(cardGameEvent);
            eventOrchestrator.RaiseEvent(cardGameEvent, this);
        }

        public void OutputMessage(string message)
        {
            OnOutputMessageSent?.Invoke(message);
        }
    }
}
