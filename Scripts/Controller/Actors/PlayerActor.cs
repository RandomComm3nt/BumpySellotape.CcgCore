namespace CcgCore.Controller.Actors
{
    public class PlayerActor : Actor
    {
        public PlayerActor(TurnSystem turnSystem, ActorScope actorScope) : base(turnSystem, actorScope)
        {
        }

        public override string Name => "Player";

        public override void DoTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}
