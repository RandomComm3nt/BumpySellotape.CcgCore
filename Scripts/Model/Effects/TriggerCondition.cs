using CcgCore.Controller.Events;
using CcgCore.Model.Effects.Conditions;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [Serializable, HideReferenceObjectPicker]
    public class TriggerCondition
    {
        [SerializeField, HideInInspector] private bool restrictToLocalScope = false;
        [SerializeField, HideInInspector] private bool hasTrigger = true;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideLabel, HorizontalGroup("@DisplayLabel/Selection"), DisableIf("restrictToLocalScope"), ValueDropdown("GetPossibleScopeTypes")] private ScopeSelectionType scopeSelectionType;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideLabel, HorizontalGroup("@DisplayLabel/Selection")] private ParameterScopeLevel parameterScopeLevel;
        [SerializeField, FoldoutGroup("@DisplayLabel"), HideReferenceObjectPicker] private List<Condition> conditions = new List<Condition>();

        public bool CheckConditions(CardGameEvent e, ParameterScope thisScope, string debugCardName = null)
        {
            if (debugCardName != null)
                Debug.Log($"{debugCardName} - testing trigger condition");
            List<ParameterScope> targetScopes = new List<ParameterScope>();
            switch (scopeSelectionType)
            {
                case ScopeSelectionType.This:
                    targetScopes.Add(thisScope.GetHigherScope(parameterScopeLevel));
                    break;
                case ScopeSelectionType.Trigger:
                    targetScopes.Add(e.GetFromHeirachyAtLevel(parameterScopeLevel));
                    break;
                case ScopeSelectionType.Any:
                    targetScopes = thisScope.GetHigherScope(ParameterScopeLevel.Global).GetAllChildScopesAtLevel(parameterScopeLevel);
                    break;
                case ScopeSelectionType.NotThis:
                    targetScopes = thisScope.RootScope.GetAllChildScopesAtLevel(parameterScopeLevel);
                    targetScopes.Remove(thisScope.GetHigherScope(parameterScopeLevel));
                    break;
            }

            if (targetScopes.Count == 0)
            {
                if (debugCardName != null)
                    Debug.Log($"{debugCardName} - no target scopes found");
                return false;
            }

            if (debugCardName != null)
                Debug.Log($"{debugCardName} - {targetScopes.Count} target scopes found");
            return targetScopes.Any(ts => conditions.TrueForAll(c => c.CheckCondition(ts)));
        }

        public bool IsLocallyScoped => scopeSelectionType == ScopeSelectionType.This;
        public enum ScopeSelectionType
        {
            This = 0,
            Trigger,
            NotThis,
            Any,
        }

#if UNITY_EDITOR
        public string DisplayLabel => $"{scopeSelectionType} {parameterScopeLevel} - " + (conditions.Count == 1 ? conditions[0].DisplayLabel : $"{conditions.Count} Conditions");

        public void SetLocalScopeRestriction(bool restricted)
        {
            restrictToLocalScope = restricted;
            if (restricted)
            {
                scopeSelectionType = ScopeSelectionType.This;
            }
        }

        public List<ScopeSelectionType> GetPossibleScopeTypes()
        {
            var list = new List<ScopeSelectionType>() { ScopeSelectionType.This };
            if (restrictToLocalScope)
                return list;
            if (hasTrigger)
                list.Add(ScopeSelectionType.Trigger);
            list.Add(ScopeSelectionType.Any);
            list.Add(ScopeSelectionType.NotThis);
            return list;
        }
#endif
    }
}
