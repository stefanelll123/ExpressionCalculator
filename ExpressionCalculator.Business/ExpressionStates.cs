namespace ExpressionCalculator.Business
{
    public enum ExpressionStates
    {
        Start,
        Number,
        Operator,
        OpenBracket,
        CloseBracket,
        End
    }
}