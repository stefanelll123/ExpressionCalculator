using ExpressionCalculator.Business.DomainServices.OperatorServices;
using ExpressionCalculator.Business.ValueObjects.Operators;

namespace ExpressionCalculator.Business.Entities
{
    internal sealed class OperatorNode : Node
    {
        public OperatorNode(Operator @operator)
        {
            Operator = @operator;
        }

        public Operator Operator { get; }
        
        public override int Calculate()
        {
            return OperatorServiceFactory.GetOperatorService(Operator).Calculate(Left.Calculate(), Right.Calculate());
        }
    }
}