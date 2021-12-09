using System.Collections.Generic;
using System.Text;

namespace ExpressionCalculator.Business.ValueObjects.Errors
{
    public sealed class ExpressionInvalidStateError : Error
    {
        public ExpressionInvalidStateError(int index, ExpressionStates currentState, IReadOnlyCollection<ExpressionStates> expectedStates)
        {
            Index = index;
            CurrentState = currentState;
            ExpectedStates = expectedStates;
        }

        public int Index { get; }
        
        public ExpressionStates CurrentState { get; }
        
        public IReadOnlyCollection<ExpressionStates> ExpectedStates { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Index;
            yield return CurrentState;
            yield return ExpectedStates;
        }

        public override string GetErrorMessage()
        {
            var expectedStatesBuilder = new StringBuilder();
            foreach (var state in ExpectedStates)
            {
                expectedStatesBuilder.Append(state.ToString());
                expectedStatesBuilder.Append(" ");
            }

            return
                $"Invalid State at index {Index}. Expected one of: {expectedStatesBuilder}, but found {CurrentState}";
        }
    }
}