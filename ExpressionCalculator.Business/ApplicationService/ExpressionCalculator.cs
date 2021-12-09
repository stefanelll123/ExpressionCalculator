namespace ExpressionCalculator.Business.ApplicationService
{
    public sealed class ExpressionCalculator : IExpressionCalculator
    {
        private readonly IExpressionFiniteAutomate expressionFiniteAutomate;
        
        public ExpressionCalculator()
        {
            expressionFiniteAutomate = new ExpressionFiniteAutomate(); // Here some DI will be better
        }

        public int Calculate(string expression)
        {
            var node = expressionFiniteAutomate.CreateExpressionTree(expression);
            
            return node.Calculate();
        }
    }
}