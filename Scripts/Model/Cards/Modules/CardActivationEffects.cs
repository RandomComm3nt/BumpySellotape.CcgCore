using CcgCore.Controller.Cards;
using CcgCore.Model.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model.Cards
{
    public class CardActivationEffects : CardDefinitionModule
    {
        [field: OdinSerialize, HideReferenceObjectPicker, FoldoutGroup("Activation Effects")]
        public List<CardEffect> ActivationEffects { get; } = new List<CardEffect>();

        [field: SerializeField, FoldoutGroup("Activation Effects")]
        public bool DestroyWhenPlayed { get; private set; } = false;

        public override void ActivateCard(CardEffectActivationContext context, Card thisCard)
        {
            ActivationEffects.ForEach(e =>
            {
                if (!context.wasActionCancelled)
                    e.ActivateEffects(context, thisCard);
            });


            if (DestroyWhenPlayed)
            {
                thisCard.ParentScope?.RemoveChild(thisCard);
            }
        }
    }
}
