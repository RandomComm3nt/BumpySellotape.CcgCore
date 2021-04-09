using CcgCore.Controller.Actors;
using CcgCore.Controller.Cards;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public abstract class TargetedCardEffect : CardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), HorizontalGroup("@DisplayLabel/ActorSelection"), LabelText("Actor"), LabelWidth(70)] protected ActorFilter actorFilter;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HorizontalGroup("@DisplayLabel/ActorSelection"), HideLabel] protected ActorSelector actorSelector;

        protected string TargetString => $"{actorFilter} {actorSelector}";

        protected List<Actor> GetTargetActors(CardEffectActivationContext context, Card thisCard)
        {
            var ts = context.cardGameController.TurnSystem;
            var actor = actorSelector switch
            {
                ActorSelector.OwnerOfThisCard => thisCard.ActorScope,
                ActorSelector.TriggerActor => context.triggerActor,
                _ => throw new NotImplementedException(),
            };
            var theActor = new List<Actor> { actor.actor };

            return actorFilter switch
            {
                ActorFilter.The => theActor,
                ActorFilter.NotThe => ts.Actors.Except(theActor).Take(1).ToList(),
                ActorFilter.All => ts.Actors,
                ActorFilter.AllExceptThe => ts.Actors.Except(theActor).ToList(),
                _ => throw new NotImplementedException(),
            };
        }

        public enum ActorFilter
        {
            The,
            NotThe,
            All,
            AllExceptThe,
        }

        public enum ActorSelector
        {
            OwnerOfThisCard,
            TriggerActor,
        }
    }
}
