using CcgCore.Controller.Events;
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
        [SerializeField, FoldoutGroup("@DisplayLabel"), ValidateInput("ValidateGlobalScopes", defaultMessage: "If trigger type is This, conditions can only reference \"This\" scope")] 
        private List<TriggerCondition> triggerConditions = new List<TriggerCondition>();
        [SerializeField, FoldoutGroup("@DisplayLabel")] private List<CardEffect> effects = new List<CardEffect>();

        public bool CheckConditions(CardGameEvent e, ParameterScope thisScope)
        {
            if (triggerFilterType == TriggerFilterType.This && e.callingHeirachy[0] != thisScope)
                return false;
            if (triggerFilterType == TriggerFilterType.NotThis && e.callingHeirachy[0] == thisScope)
                return false;

            return triggerConditions.TrueForAll(c => c.CheckConditions(e, thisScope));
        }

        public void ActivateEffect(ParameterScope thisScope, CardEffectActivationContextBase context)
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
            Any
        }

#if UNITY_EDITOR
        private string DisplayLabel => $"On {trigger} from {triggerFilterType}";

        private bool ValidateGlobalScopes => triggerFilterType != TriggerFilterType.This || triggerConditions.TrueForAll(tc => tc.IsLocallyScoped);

        private void ToggleConditions()
        {
            triggerConditions.ForEach(tc => tc.SetLocalScopeRestriction(triggerFilterType == TriggerFilterType.This));
        }
#endif
    }
}
