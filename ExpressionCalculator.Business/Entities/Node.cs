using ExpressionCalculator.Business.ValueObjects;

namespace ExpressionCalculator.Business.Entities
{
    public abstract class Node
    {
        public Node Left { get; set; }

        public Node Right { get; set; }

        public abstract int Calculate();
    }
}