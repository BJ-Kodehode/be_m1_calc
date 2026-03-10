public class Calculator : ICalculator
{
    public double AddNumbers(double a, double b) => a + b;

    public double SubtractNumbers(double a, double b) => a - b;

    public double MultiplyNumbers(double a, double b) => a * b;

    public double DivideNumbers(double a, double b)
    {
        if (b == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Feil: Deling på null er ikke tillatt.");
            Console.ResetColor();
            return double.NaN;
        }
        return a / b;
    }
}