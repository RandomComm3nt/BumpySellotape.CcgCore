using CcgCore.Controller.Cards;
using CcgCore.Model;
using Stats.Controller;
using UnityEngine;

namespace CcgCore.Controller.Actors
{
    public abstract class Actor
    {
        private bool isTurn;
        private int cardsPlayedThisTurn;
        private readonly TurnSystem turnSystem;

        public virtual string Name { get; } = "-";
        public ActorScope ActorScope { get; }
        public StatCollection StatCollection { get; private set; }
        public ActorTemplate ActorTemplate { get; private set; }

        public Actor(TurnSystem turnSystem, ActorScope actorScope)
        {
            this.turnSystem = turnSystem;
            turnSystem.AddActor(this);
            ActorScope = actorScope;
            StatCollection = new StatCollection();
            StatCollection.OnAnyStatValueChange += () => actorScope.RaiseEvent(new Events.CardGameEvent(Events.EventType.StatChanged));
        }

        public void StartTurn()
        {
            isTurn = true;
            cardsPlayedThisTurn = 0;
            ActorScope.RaiseEvent(new Events.CardGameEvent(Events.EventType.TurnStart));
        }

        public abstract void DoTurn();

        public void EndTurn()
        {
            isTurn = false;
            ActorScope.RaiseEvent(new Events.CardGameEvent(Events.EventType.TurnEnd));
        }

        public void SetTemplate(ActorTemplate actorTemplate)
        {
            ActorTemplate = actorTemplate;
            ActorScope.Initialise(this, actorTemplate);

            StatCollection.GenerateFromTemplates(actorTemplate.StatTemplates);
        }

        public void PlayCard(Card card)
        {
            if (!isTurn)
            {
                Debug.LogWarning("Tried to play a card when it was not the actor's turn");
                return;
            }

            card.AttemptPlayCard(null);
            cardsPlayedThisTurn++;
            CheckIfShouldEndTurn();
        }

        protected virtual void CheckIfShouldEndTurn()
        {
            if (isTurn && cardsPlayedThisTurn == 1)
                turnSystem.EndCurrentTurn();
        }
    }
}
