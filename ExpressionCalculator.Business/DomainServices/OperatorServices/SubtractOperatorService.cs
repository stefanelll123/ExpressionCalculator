namespace ExpressionCalculator.Business.DomainServices.OperatorServices
{
    internal sealed class SubtractOperatorService : OperatorService
    {
        public override int Calculate(int firstValue, int secondValue)
        {
            return firstValue - secondValue;
        }
    }
}