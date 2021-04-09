using CcgCore.Controller.Actors;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public abstract class TargetedCardEffect : CardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] protected ActivationEffectTargetType target = ActivationEffectTargetType.Activator;

        protected List<Actor> GetTargetActors(CardEffectActivationContext context)
        {
            var ts = context.cardGameController.TurnSystem;
            return target switch
            {
                ActivationEffectTargetType.Activator => new List<Actor> { ts.CurrentTurnActor },
                ActivationEffectTargetType.NextActor => new List<Actor> { ts.NextTurnActor },
                _ => throw new NotImplementedException(),
            };
        }
    }

    public enum ActivationEffectTargetType
    {
        Activator = 0,
        NextActor,
        AllActors,
        AllActorsExceptCurrent,
        RandomActor,
        RandomActorExceptCurrent,
    }
}
