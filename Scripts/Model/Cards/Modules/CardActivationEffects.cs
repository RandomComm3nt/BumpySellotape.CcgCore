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
    }
}
