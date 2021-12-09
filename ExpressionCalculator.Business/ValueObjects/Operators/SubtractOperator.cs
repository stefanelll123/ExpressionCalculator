using System;

namespace ExpressionCalculator.Business.ValueObjects.Operators
{
    public sealed class SubtractOperator : Operator
    {
        public SubtractOperator(Index index) : base(index)
        {
        }
        
        public override int GetDominantIndex()
        {
            return 2;
        }
    }
}