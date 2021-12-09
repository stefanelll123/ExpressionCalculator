namespace ExpressionCalculator.Business.Entities
{
    internal sealed class OperandNode : Node
    {
        public OperandNode(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override int Calculate()
        {
            return Value;
        }
    }
}