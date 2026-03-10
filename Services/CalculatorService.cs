using System.Globalization;

public class CalculatorService
{
    private readonly ICalculator _calculator;

    public CalculatorService()
    {
        _calculator = new Calculator();
    }

    public void RunCalculator()
    {
        Console.WriteLine("Velkommen til Kalkulator-appen!");
        Console.WriteLine("Skriv 'exit' eller 'quit' for å avslutte.");

        while (true)
        {
            double num1 = GetValidNumber("Skriv første tall:");
            string? op = GetValidOperator();
            double num2 = GetValidNumber("Skriv andre tall:");

            double result = Calculate(num1, op, num2);

            if (!double.IsNaN(result))
            {
                Console.WriteLine($"Resultat: {result}");
            }

            Console.WriteLine("Vil du gjøre en ny beregning? (ja/nei)");
            var continueInput = Console.ReadLine()?.Trim().ToLower();
            if (continueInput != "ja" && continueInput != "j")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Avslutter kalkulatoren...");
                Console.ResetColor();
                break;
            }
        }
    }

    private double Calculate(double num1, string op, double num2)
    {
        return op switch
        {
            "+" => _calculator.AddNumbers(num1, num2),
            "-" => _calculator.SubtractNumbers(num1, num2),
            "*" => _calculator.MultiplyNumbers(num1, num2),
            "/" => _calculator.DivideNumbers(num1, num2),
            _ => throw new ArgumentException("Ugyldig operatør")
        };
    }

    private double GetValidNumber(string message)
    {
        double number;
        while (true)
        {
            Console.WriteLine(message);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
            {
                return number;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ugyldig input. Vennligst skriv et gyldig tall.");
            Console.ResetColor();
        }
    }

    private string GetValidOperator()
    {
        while (true)
        {
            Console.WriteLine("Skriv operatør (+, -, *, /):");
            string? op = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(op) && "+-*/".Contains(op) && op.Length == 1)
            {
                return op;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ugyldig operatør! Vennligst skriv +, -, * eller /:");
            Console.ResetColor();
        }
    }
}