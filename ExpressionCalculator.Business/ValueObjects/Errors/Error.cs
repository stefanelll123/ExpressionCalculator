namespace ExpressionCalculator.Business.ValueObjects.Errors
{
    public abstract class Error : ValueObject
    {
        public abstract string GetErrorMessage();
    }
}