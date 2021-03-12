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
    public class TriggerCondition
    {
        [SerializeField, HideInInspector] private bool restrictToLocalScope = false;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideLabel, HorizontalGroup("@DisplayLabel/Selection"), DisableIf("restrictToLocalScope")] private ScopeSelectionType scopeSelectionType;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideLabel, HorizontalGroup("@DisplayLabel/Selection")] private ParameterScopeLevel parameterScopeLevel;
        [SerializeField, FoldoutGroup("@DisplayLabel")] private List<Condition> conditions = new List<Condition>();

        public bool CheckConditions(CardGameEvent e, ParameterScope thisScope)
        {
            var targetScope = scopeSelectionType == ScopeSelectionType.This ? thisScope.GetHigherScope(parameterScopeLevel) : e.GetFromHeirachyAtLevel(parameterScopeLevel);
            if (targetScope == null)
                return false;
            return conditions.TrueForAll(c => c.CheckCondition(targetScope));
        }

        public bool IsLocallyScoped => scopeSelectionType == ScopeSelectionType.This;
        public enum ScopeSelectionType
        {
            This = 0,
            Trigger
        }

#if UNITY_EDITOR
        private string DisplayLabel => $"{scopeSelectionType} {parameterScopeLevel} - {conditions.Count} Conditions";

        public void SetLocalScopeRestriction(bool restricted)
        {
            restrictToLocalScope = restricted;
            if (restricted)
            {
                scopeSelectionType = ScopeSelectionType.This;
            }
        }
#endif
    }
}
