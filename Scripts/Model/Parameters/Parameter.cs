using System;
using UnityEngine;

namespace CcgCore.Model.Parameters
{
    public abstract class Parameter<T> : ScriptableObject
    {
        [SerializeField] private ParameterScopeLevel scopeLevel = ParameterScopeLevel.Global;

        [field: SerializeField] protected T DefaultValue { get; private set; }
        [field: SerializeField] protected T MinValue { get; private set; }
        [field: SerializeField] protected T MaxValue { get; private set; }

        public T Evaluate(ParameterScope scope)
        {
            var newScope = scope;
            // if we haven't been given the right scope, look for it at a higher level
            if (scope.ScopeLevel != scopeLevel)
            {
                newScope = scope.GetHigherScope(scopeLevel);
            }
            if (newScope == null)
                throw new Exception($"Could not find scope at level {scopeLevel}. Object passed was at level {scope.ScopeLevel}.");

            return EvaluateInner(newScope);
        }

        protected abstract T EvaluateInner(ParameterScope scope);
    }
}
