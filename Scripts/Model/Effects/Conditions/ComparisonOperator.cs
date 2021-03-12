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
        public static bool CompareValue(int input, ComparisonOperator comparisonOperator, int value)
        {
            switch (comparisonOperator)
            {
                case ComparisonOperator.Equals:
                    return input == value;
                case ComparisonOperator.NotEquals:
                    return input != value;
                case ComparisonOperator.GreaterThan:
                    return input > value;
                case ComparisonOperator.GreaterThanOrEquals:
                    return input >= value;
                case ComparisonOperator.LessThan:
                    return input < value;
                case ComparisonOperator.LessThanOrEquals:
                    return input <= value;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
