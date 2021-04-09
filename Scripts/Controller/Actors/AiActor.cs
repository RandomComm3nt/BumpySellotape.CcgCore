namespace CcgCore.Controller.Actors
{
    public class AiActor : Actor
    {
        public AiActor(TurnSystem turnSystem, ActorScope actorScope) : base(turnSystem, actorScope)
        {
        }

        public override string Name => "AI";

        public override void DoTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}
