using System;

namespace CcgCore.Model.Effects.Conditions
{
    public enum ComparisonOperator
    {
        Equals = 0,
        NotEquals,
        GreaterThan,
        GreaterThanOrEquals,
        LessThan,
        LessThanOrEquals,
    }

    public static class ComparisonUtility
    {
        public static string GetDisplayString(ComparisonOperator comparisonOperator)
        {
            return comparisonOperator switch
            {
                ComparisonOperator.Equals => "=",
                ComparisonOperator.NotEquals => "!=",
                ComparisonOperator.GreaterThan => ">",
                ComparisonOperator.GreaterThanOrEquals => ">=",
                ComparisonOperator.LessThan => "<",
                ComparisonOperator.LessThanOrEquals => "<=",
                _ => throw new NotImplementedException(),
            };
        }

        public static bool CompareValue(int input, ComparisonOperator comparisonOperator, int value)
        {
            return comparisonOperator switch
            {
                ComparisonOperator.Equals => input == value,
                ComparisonOperator.NotEquals => input != value,
                ComparisonOperator.GreaterThan => input > value,
                ComparisonOperator.GreaterThanOrEquals => input >= value,
                ComparisonOperator.LessThan => input < value,
                ComparisonOperator.LessThanOrEquals => input <= value,
                _ => throw new NotImplementedException(),
            };
        }

        public static bool CompareValue(float input, ComparisonOperator comparisonOperator, float value)
        {
            return comparisonOperator switch
            {
                ComparisonOperator.Equals => input == value,
                ComparisonOperator.NotEquals => input != value,
                ComparisonOperator.GreaterThan => input > value,
                ComparisonOperator.GreaterThanOrEquals => input >= value,
                ComparisonOperator.LessThan => input < value,
                ComparisonOperator.LessThanOrEquals => input <= value,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
