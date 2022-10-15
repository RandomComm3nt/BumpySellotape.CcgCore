using BumpySellotape.Events.Model.Effects;
using CcgCore.Controller.Cards;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Cards
{
    public class CardActivationEffects : CardDefinitionModule
    {
        [field: OdinSerialize, HideReferenceObjectPicker]
        public List<IEffect> ActivationEffects { get; } = new();

        //TODO: Don't like this, get rid?
        [field: SerializeField, FoldoutGroup("Activation Effects"), HideInInspector]
        public bool DestroyWhenPlayed { get; private set; } = false;

        public override void ActivateCard(ProcessingContext context, Card thisCard)
        {
            context.effectsToProcess = ActivationEffects.ToList();
            context.ProcessNextEffect();

            //TODO: Don't like this, get rid?
            if (DestroyWhenPlayed)
            {
                thisCard.ParentScope?.RemoveChild(thisCard);
            }
        }
    }
}
