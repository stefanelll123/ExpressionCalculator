using System;

namespace ExpressionCalculator
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            var input = "{[6+9]*2+5*(3-2)}";
            var calculator = new Business.ApplicationService.ExpressionCalculator();
            
            Console.WriteLine(calculator.Calculate(input));
        }
    }
}