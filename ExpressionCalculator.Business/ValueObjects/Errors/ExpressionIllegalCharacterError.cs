using System.Collections.Generic;

namespace ExpressionCalculator.Business.ValueObjects.Errors
{
    public sealed class ExpressionIllegalCharacterError : Error
    {
        public ExpressionIllegalCharacterError(int index, char illegalCharacter)
        {
            Index = index;
            IllegalCharacter = illegalCharacter;
        }

        public int Index { get; }
        
        public char IllegalCharacter { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Index;
            yield return IllegalCharacter;
        }

        public override string GetErrorMessage()
        {
            return $"Unexpected token {IllegalCharacter} at index {Index}.";
        }
    }
}