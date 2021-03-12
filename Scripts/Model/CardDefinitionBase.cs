using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model
{
    public abstract class CardDefinitionBase : SerializedScriptableObject
    {
        [field: SerializeField, ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")]
        public List<int> Tags { get; private set; } = new List<int>();

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: OdinSerialize, HideReferenceObjectPicker, FoldoutGroup("Active Effects", VisibleIf = "CanCardHaveActiveEffects")]
        public List<CardEffect> ActivationEffects { get; } = new List<CardEffect>();

        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects", VisibleIf = "CanCardExistOnField")] 
        public List<IntParameterModifier> WhileActiveIntModifiers = new List<IntParameterModifier>();
        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects")]
        public List<FloatParameterModifier> WhileActiveFloatModifiers = new List<FloatParameterModifier>();
        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects")]
        public List<TriggeredEffect> TriggeredEffects = new List<TriggeredEffect>();

        protected virtual bool CanCardExistOnField => true;
        protected virtual bool CanCardHaveActiveEffects => true;
    }
}