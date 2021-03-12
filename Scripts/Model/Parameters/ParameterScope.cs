﻿using CcgCore.Controller.Events;
using CcgCore.Model.Effects;
using System.Collections.Generic;
using System.Linq;

namespace CcgCore.Model.Parameters
{
    public abstract class ParameterScope
    {
        public ParameterScopeLevel ScopeLevel { get; }
        public ParameterScope ParentScope { get; private set; }
        public List<ParameterScope> ChildScopes { get; }

        protected ParameterScope(ParameterScopeLevel scopeLevel, ParameterScope parentScope)
        {
            ScopeLevel = scopeLevel;
            ParentScope = parentScope;
            if (parentScope != null)
                parentScope.RegisterChildScope(this);
            ChildScopes = new List<ParameterScope>();
        }

        public void RegisterChildScope(ParameterScope scope)
        {
            ChildScopes.Add(scope);
        }

        public ParameterScope GetHigherScope(ParameterScopeLevel scopeLevel)
        {
            if (scopeLevel == ScopeLevel)
                return this;
            if (ParentScope == null)
                return null;
            return ParentScope.GetHigherScope(scopeLevel);
        }

        public virtual void RaiseEvent(CardGameEvent cardGameEvent)
        {
            cardGameEvent.callingHeirachy.Add(this);
            ParentScope?.RaiseEvent(cardGameEvent);
        }

        public List<(ParameterScope scope, TriggeredEffect effect)> GetAllTriggeredEffectsForEvent(CardGameEvent cardGameEvent)
        {
            var effects = GetTriggeredEffectsForEvent(cardGameEvent);
            effects.AddRange(ChildScopes.SelectMany(s => s.GetAllTriggeredEffectsForEvent(cardGameEvent)));
            return effects;
        }

        protected virtual List<(ParameterScope scope, TriggeredEffect effect)> GetTriggeredEffectsForEvent(CardGameEvent cardGameEvent)
        {
            return new List<(ParameterScope scope, TriggeredEffect effect)>();
        }

        public void SetParentScope(ParameterScope scope)
        {
            // TECH DEBT - unregister previous parent
            ParentScope = scope;
            scope.RegisterChildScope(this);
        }

        public void RemoveChild(ParameterScope scope)
        {
            ChildScopes.Remove(scope);
            scope.ParentScope = null;
        }
    }
}
