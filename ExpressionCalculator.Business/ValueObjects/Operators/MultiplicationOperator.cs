using System;

namespace ExpressionCalculator.Business.ValueObjects.Operators
{
    public sealed class MultiplicationOperator : Operator
    {
        public MultiplicationOperator(Index index) : base(index)
        {
        }
        
        public override int GetDominantIndex()
        {
            return 1;
        }
    }
}