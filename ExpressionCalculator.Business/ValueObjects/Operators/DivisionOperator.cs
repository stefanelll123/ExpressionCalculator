using System;

namespace ExpressionCalculator.Business.ValueObjects.Operators
{
    public sealed class DivisionOperator : Operator
    {
        public DivisionOperator(Index index) : base(index)
        {
        }
        
        public override int GetDominantIndex()
        {
            return 1;
        }
    }
}