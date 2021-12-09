using System;
using System.Collections.Generic;

namespace ExpressionCalculator.Business.ValueObjects.Operators
{
    public abstract class Operator : ValueObject
    {
        protected Operator(Index index)
        {
            Index = index;
        }

        public Index Index { get; }
        
        public abstract int GetDominantIndex();
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Index;
        }
    }
}