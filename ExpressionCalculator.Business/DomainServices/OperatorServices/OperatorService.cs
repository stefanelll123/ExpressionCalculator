namespace ExpressionCalculator.Business.DomainServices.OperatorServices
{
    internal abstract class OperatorService
    {
        public abstract int Calculate(int firstValue, int secondValue);
    }
}