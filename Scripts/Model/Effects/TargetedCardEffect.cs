using BumpySellotape.TurnBased.Controller.Actors;
using CcgCore.Controller.Actors;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public abstract class TargetedCardEffect : CardEffect
    {
        [SerializeField, HorizontalGroup("@DisplayLabel/ActorSelection"), HideIf("HideTargettingFields"), LabelText("Actor"), LabelWidth(70)] protected ActorFilter actorFilter;
        [SerializeField, HorizontalGroup("@DisplayLabel/ActorSelection"), HideIf("HideTargettingFields"), HideLabel] protected ActorSelector actorSelector;

        protected string TargetString => $"{actorFilter} {actorSelector}";
        protected virtual bool HideTargettingFields => false; 

        protected List<ActorScope> GetTargetActors(CardEffectActivationContext context, ParameterScope thisScope)
        {
            var allActorScopes = thisScope.RootScope.GetAllChildScopesOfType<ActorScope>();
            var actor = actorSelector switch
            {
                ActorSelector.OwnerOfThisCard => thisScope.ActorScope,
                ActorSelector.TriggerActor => context.triggerActor,
                _ => throw new NotImplementedException(),
            };
            var theActor = new List<ActorScope> { actor };

            return actorFilter switch
            {
                ActorFilter.The => theActor,
                ActorFilter.NotThe => allActorScopes.Except(theActor).Take(1).ToList(),
                ActorFilter.All => allActorScopes,
                ActorFilter.AllExceptThe => allActorScopes.Except(theActor).ToList(),
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
