using System;

namespace ExpressionCalculator.Business.ValueObjects.Operators
{
    public sealed class AdditionOperator : Operator
    {
        public AdditionOperator(Index index) : base(index)
        {
        }

        public override int GetDominantIndex()
        {
            return 2;
        }
    }
}