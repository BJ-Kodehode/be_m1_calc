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
        Console.WriteLine("Du kan også kryptere/dekryptere resultater.");

        while (true)
        {
            Console.WriteLine("\nVelg modus:");
            Console.WriteLine("1. Utfør beregning");
            Console.WriteLine("2. Dekrypter en kryptert streng");
            Console.WriteLine("3. Avslutt");
            Console.Write("Ditt valg: ");
            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    PerformCalculation();
                    break;
                case "2":
                    DecryptString();
                    break;
                case "3":
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Avslutter kalkulatoren...");
                    Console.ResetColor();
                    return;
            }
        }
    }

    private void PerformCalculation()
    {
        double num1 = GetValidNumber("Skriv første tall:");
        string? op = GetValidOperator();
        double num2 = GetValidNumber("Skriv andre tall:");

        double result = Calculate(num1, op, num2);

        if (!double.IsNaN(result))
        {
            Console.WriteLine($"Resultat: {result}");

            Console.Write("Vil du kryptere resultatet? (ja/nei): ");
            var encryptChoice = Console.ReadLine()?.Trim().ToLower();
            if (encryptChoice == "ja" || encryptChoice == "j")
            {
                string encrypted = EncryptionHelper.Encrypt(result.ToString());
                Console.WriteLine($"Kryptert resultat: {encrypted}");
            }
        }

        Console.Write("Vil du gjøre en ny beregning? (ja/nei): ");
        var continueInput = Console.ReadLine()?.Trim().ToLower();
        if (continueInput != "ja" && continueInput != "j")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tilbake til hovedmeny...");
            Console.ResetColor();
        }
    }

    private void DecryptString()
    {
        Console.Write("Skriv den krypterte strengen: ");
        var encrypted = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(encrypted))
        {
            try
            {
                string decrypted = EncryptionHelper.Decrypt(encrypted);
                Console.WriteLine($"Dekryptert: {decrypted}");
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ugyldig kryptert streng.");
                Console.ResetColor();
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