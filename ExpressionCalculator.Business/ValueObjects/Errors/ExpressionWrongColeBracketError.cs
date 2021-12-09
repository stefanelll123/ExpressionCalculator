using System.Collections.Generic;

namespace ExpressionCalculator.Business.ValueObjects.Errors
{
    public sealed class ExpressionWrongCloseBracketError : Error
    {
        public ExpressionWrongCloseBracketError(int index, char currentCloseBracket, char expectedCloseBracket)
        {
            Index = index;
            CurrentCloseBracket = currentCloseBracket;
            ExpectedCloseBracket = expectedCloseBracket;
        }

        public int Index { get; }
        
        public char CurrentCloseBracket { get; }
        
        public char ExpectedCloseBracket { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Index;
            yield return CurrentCloseBracket;
            yield return ExpectedCloseBracket;
        }

        public override string GetErrorMessage()
        {
            return
                $"Wrong close bracket at index {Index}. Expected {ExpectedCloseBracket}, but found {CurrentCloseBracket}.";
        }
    }
}