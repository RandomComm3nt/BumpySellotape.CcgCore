using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using CcgCore.Model.Special;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model.Cards
{
    public class CardPassiveEffects : CardDefinitionModule
    {
        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects", VisibleIf = "CanCardExistOnField")]
        public List<IntParameterModifier> WhileActiveIntModifiers = new List<IntParameterModifier>();
        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects")]
        public List<FloatParameterModifier> WhileActiveFloatModifiers = new List<FloatParameterModifier>();
        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects")]
        public List<TriggeredEffect> TriggeredEffects = new List<TriggeredEffect>();
        [field: SerializeField, FoldoutGroup("Passive Effects")]
        public List<CompositeValueThreshold> ThresholdModifiers { get; private set; } = new List<CompositeValueThreshold>();
        [field: SerializeField, FoldoutGroup("Passive Effects"), HideReferenceObjectPicker] public StackProtectionEffect StackProtectionEffect { get; private set; } = new StackProtectionEffect();
    }
}
