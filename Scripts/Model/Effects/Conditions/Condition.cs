using CcgCore.Model.Parameters;
using System;

namespace CcgCore.Model.Effects.Conditions
{
    public abstract class Condition
    {
        public abstract bool CheckCondition(ParameterScope scope);
    }

    public abstract class Condition<TScope> : Condition
        where TScope : ParameterScope
    {
        public override bool CheckCondition(ParameterScope scope)
        {
            if (scope is TScope s)
            {
                return CheckConditionInternal(s);
            }
            throw new Exception("Input was not of correct type");
        }

        protected abstract bool CheckConditionInternal(TScope scope);
    }
}
