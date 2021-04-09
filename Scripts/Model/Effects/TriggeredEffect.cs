using CcgCore.Controller.Events;
using CcgCore.Model.Effects.Conditions;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [Serializable, HideReferenceObjectPicker]
    public class TriggeredEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel"), LabelText("On")] private CardEffectTrigger trigger = CardEffectTrigger.CardActivation;
        [SerializeField, FoldoutGroup("@DisplayLabel"), OnValueChanged("ToggleConditions"), LabelText("Where trigger is"), SuffixLabel("scope")] 
        private TriggerFilterType triggerFilterType = TriggerFilterType.This;
        [Tooltip("The parent level to check up to see if the scope is related to this scope")]
        [SerializeField, FoldoutGroup("@DisplayLabel"), ShowIf("triggerFilterType", TriggerFilterType.RelatedToThis), ShowIf("triggerFilterType", TriggerFilterType.NotRelatedToThis)] 
        private ParameterScopeLevel sharedParentLevel;
        [SerializeField, FoldoutGroup("@DisplayLabel"), ValidateInput("ValidateGlobalScopes", defaultMessage: "If trigger type is This, conditions can only reference \"This\" scope")]
        private List<TriggerCondition> triggerConditions = new List<TriggerCondition>();
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideReferenceObjectPicker]
        private List<CalculationCondition> calculationConditions = new List<CalculationCondition>();
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideReferenceObjectPicker] private List<CardEffect> effects = new List<CardEffect>();

        public bool CheckConditions(CardGameEvent e, ParameterScope thisScope, string debugCardName = null)
        {
            if (debugCardName != null)
                Debug.Log($"{debugCardName} - checking conditions for event {e}");
            // TECH DEBT - not checking event type

            ParameterScope triggerScope = e.callingHeirachy[0];
            if (triggerFilterType == TriggerFilterType.This && triggerScope != thisScope)
                return false;
            if (triggerFilterType == TriggerFilterType.NotThis && triggerScope == thisScope)
                return false;
            if (triggerFilterType == TriggerFilterType.RelatedToThis && triggerScope.GetHigherScope(sharedParentLevel) != thisScope.GetHigherScope(sharedParentLevel))
                return false;
            if (triggerFilterType == TriggerFilterType.NotRelatedToThis && triggerScope.GetHigherScope(sharedParentLevel) == thisScope.GetHigherScope(sharedParentLevel))
                return false;

            if (debugCardName != null)
                Debug.Log($"{debugCardName} - triggerFilterType condition met");
            return triggerConditions.TrueForAll(c => c.CheckConditions(e, thisScope, debugCardName));
        }

        public void ActivateEffect(ParameterScope thisScope, CardEffectActivationContext context, string debugCardName = null)
        {
            foreach (var e in effects)
            {
                e.ActivateEffects(context);
            }
        }

        public enum TriggerFilterType
        {
            This = 0,
            NotThis,
            Any,
            RelatedToThis,
            NotRelatedToThis,
        }

#if UNITY_EDITOR
        private string DisplayLabel => $"On {trigger} from {triggerFilterType} scope";

        private bool ValidateGlobalScopes => triggerFilterType != TriggerFilterType.This || triggerConditions.TrueForAll(tc => tc.IsLocallyScoped);

        private void ToggleConditions()
        {
            triggerConditions.ForEach(tc => tc.SetLocalScopeRestriction(triggerFilterType == TriggerFilterType.This));
        }
#endif
    }
}
