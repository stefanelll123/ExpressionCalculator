using System.Collections.Generic;

namespace ExpressionCalculator.Business.ValueObjects
{
    public sealed class Token : ValueObject
    {
        public Token(string value, int startIndex, int endIndex)
        {
            Value = value;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public string Value { get; }
        
        public int StartIndex { get; }
        
        public int EndIndex { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartIndex;
            yield return EndIndex;
            yield return Value;
        }
    }
}