using System.Collections.Generic;

namespace CcgCore.Controller.Actors
{
    public class TurnSystem
    {
        private readonly List<Actor> actors;
        private int turnIndex;
        public Actor CurrentTurnActor => actors[turnIndex];
        public Actor NextTurnActor => actors[(turnIndex + 1) % actors.Count];

        public delegate void TurnChanged(Actor currentTurnActor);
        public event TurnChanged OnTurnChanged;

        public TurnSystem()
        {
            actors = new List<Actor>();
        }

        public void AddActor(Actor actor)
        {
            actors.Add(actor);
        }

        public void Start()
        {
            turnIndex = 0;
            CurrentTurnActor.StartTurn();
        }

        public void EndCurrentTurn()
        {
            CurrentTurnActor.EndTurn();
            turnIndex = (turnIndex + 1) % actors.Count;
            OnTurnChanged?.Invoke(CurrentTurnActor);
            CurrentTurnActor.StartTurn();
        }
    }
}
