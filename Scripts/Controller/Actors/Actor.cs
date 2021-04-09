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

        public Actor(TurnSystem turnSystem, ActorScope actorScope)
        {
            this.turnSystem = turnSystem;
            turnSystem.AddActor(this);
            ActorScope = actorScope;
            actorScope.actor = this;
            StatCollection = new StatCollection();
        }

        public virtual string Name { get; } = "-";
        public ActorScope ActorScope { get; }
        public StatCollection StatCollection { get; private set; }

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

        public void SetTemplate(ActorTemplate playerTemplate)
        {
            for (int i = 0; i < playerTemplate.Regions.Count; i++)
            {
                // TECH DEBT - Matching regions by index is bad, casting to FieldRegion is bad
                foreach (var cd in playerTemplate.Regions[i].Cards)
                    (ActorScope.ChildScopes[i] as FieldRegion).AddCard(cd);
            }


            StatCollection.GenerateFromTemplates(playerTemplate.StatTemplates);
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
