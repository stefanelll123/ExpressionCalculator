using System;
using System.Collections.Generic;

namespace ExpressionCalculator.Business.ValueObjects.Operators
{
    public static class OperatorFactory
    {
        private static readonly IReadOnlyDictionary<char, Func<int, Operator>> operators = new Dictionary<char, Func<int, Operator>>
        {
            {'+', index => new AdditionOperator(index)},
            {'-', index => new SubtractOperator(index)},
            {'*', index => new MultiplicationOperator(index)},
            {'/', index => new DivisionOperator(index)}
        };
        
        public static Operator GetOperator(char op, int index)
        {
            return operators[op](index);
        }
    }
}