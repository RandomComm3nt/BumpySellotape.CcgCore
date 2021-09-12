using BumpySellotape.TurnBased.Controller;
using BumpySellotape.TurnBased.Controller.Actors;
using CcgCore.Controller.Cards;
using CcgCore.Controller.Events;
using CcgCore.Model;
using CcgCore.Model.Parameters;
using UnityEngine;

namespace CcgCore.Controller.Actors
{
    public class ActorScope : ParameterScope
    {
        private int cardsPlayedThisTurn;
        public Actor Actor { get; private set; }

        public ActorScope(ParameterScope parentScope) 
            : base(ParameterScopeLevel.Actor, parentScope)
        {
            Actor.StatCollection.OnAnyStatChanged += () => RaiseEvent(new CardGameEvent(Events.EventType.StatChanged));
        }

        public void Initialise(Actor actor, ActorTemplate playerTemplate)
        {
            Actor = actor;
            for (int i = 0; i < playerTemplate.Regions.Count; i++)
            {
                var region = new FieldRegion(playerTemplate.Regions[i].TemplateScope, this);

                foreach (var cd in playerTemplate.Regions[i].Cards)
                    region.AddCard(cd);
            }
        }

        public void PlayCard(Card card)
        {
            if (Actor.ActorState != ActorState.DoingAction)
            {
                Debug.LogWarning("Tried to play a card when it was not the actor's turn");
                return;
            }

            card.AttemptPlayCard(null);
            cardsPlayedThisTurn++;
            CheckIfShouldEndTurn();
        }

        public void CheckIfShouldEndTurn()
        {
            if (Actor.ActorState == ActorState.ActionTaken && cardsPlayedThisTurn == 1)
                Actor.EndCurrentTurn();
            else
                Actor.BeginActionSelection();
        }

        public void StartTurn()
        {
            cardsPlayedThisTurn = 0;
            RaiseEvent(new Events.CardGameEvent(Events.EventType.TurnStart));
        }

        public void EndTurn()
        {
            RaiseEvent(new Events.CardGameEvent(Events.EventType.TurnEnd));
        }

        /*
        protected override List<(ParameterScope scope, TriggeredEffect effect)> GetTriggeredEffectsForEvent(CardGameEvent cardGameEvent)
        {
            if (Actor == null || !Actor.ActorTemplate)
                return base.GetTriggeredEffectsForEvent(cardGameEvent);
            return Actor.ActorTemplate.TriggeredEffects
                .Where(te => te.CheckConditions(cardGameEvent, this))
                .Select(te => (this as ParameterScope, te))
                .ToList();
        }
        */
    }
}
