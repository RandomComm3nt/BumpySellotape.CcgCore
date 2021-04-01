using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using CcgCore.Model.Special;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Cards
{
    [CreateAssetMenu(menuName = "Card/Card Definition")]
    public class CardDefinition : SerializedScriptableObject
    {
        [field: SerializeField, ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")]
        public List<int> Tags { get; private set; } = new List<int>();

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: OdinSerialize, HideReferenceObjectPicker, FoldoutGroup("Active Effects", VisibleIf = "CanCardHaveActiveEffects")]
        public List<CardEffect> ActivationEffects { get; } = new List<CardEffect>();
        [field: SerializeField, ShowIf("isAction")] 
        public List<CardDefinition> PossibleTargets { get; private set; } = new List<CardDefinition>();

        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects", VisibleIf = "CanCardExistOnField")] 
        public List<IntParameterModifier> WhileActiveIntModifiers = new List<IntParameterModifier>();
        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects")]
        public List<FloatParameterModifier> WhileActiveFloatModifiers = new List<FloatParameterModifier>();
        [field: OdinSerialize, NonSerialized, FoldoutGroup("Passive Effects")]
        public List<TriggeredEffect> TriggeredEffects = new List<TriggeredEffect>();
        [field: SerializeField, FoldoutGroup("Passive Effects")] 
        public List<CompositeValueThreshold> ThresholdModifiers { get; private set; } = new List<CompositeValueThreshold>();
        [field: SerializeField, FoldoutGroup("Passive Effects")] public StackProtectionEffect StackProtectionEffect { get; private set; } = new StackProtectionEffect();

        protected virtual bool CanCardExistOnField => true;
        protected virtual bool CanCardHaveActiveEffects => true;

        [field: OdinSerialize] public List<CardDefinitionModule> Modules { get; private set; } = new List<CardDefinitionModule>();

        public T GetModule<T>() where T : CardDefinitionModule
        {
            return Modules.FirstOrDefault(m => m.GetType() == typeof(T)) as T;
        }
    }
}