using System;
using System.Globalization;
using be_m1_calc;
using be_m1_calc.Encryption;
using be_m1_calc.Classes;

namespace be_m1_calc.Services;

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
            Console.WriteLine("1. Utfør grunnleggende beregning (+, -, *, /)");
            Console.WriteLine("2. Utfør avansert beregning (sin, cos, tan, sqrt, etc.)");
            Console.WriteLine("3. Dekrypter en kryptert streng");
            Console.WriteLine("4. Avslutt");
            Console.Write("Ditt valg: ");
            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    PerformCalculation();
                    break;
                case "2":
                    PerformAdvancedCalculation();
                    break;
                case "3":
                    DecryptString();
                    break;
                case "4":
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Avslutter kalkulatoren...");
                    Console.ResetColor();
                    return;
            }
        }
    }

    private void PerformAdvancedCalculation()
    {
        Console.WriteLine("\nVelg avansert funksjon:");
        Console.WriteLine("1. Sinus (sin)");
        Console.WriteLine("2. Cosinus (cos)");
        Console.WriteLine("3. Tangens (tan)");
        Console.WriteLine("4. Kvadratrot (sqrt)");
        Console.WriteLine("5. Potens (^)");
        Console.WriteLine("6. Naturlig logaritme (ln)");
        Console.WriteLine("7. Konstant e");
        Console.WriteLine("8. Konstant π (pi)");
        Console.Write("Ditt valg: ");
        var funcChoice = Console.ReadLine()?.Trim();

        double result = 0;
        bool valid = true;

        switch (funcChoice)
        {
            case "1":
                double angleSin = GetValidNumber("Skriv vinkel:");
                Console.Write("Er vinkelen i grader? (ja/nei, standard ja): ");
                var sinDeg = Console.ReadLine()?.Trim().ToLower() != "nei";
                result = _calculator.Sin(angleSin, sinDeg);
                Console.WriteLine($"Sin({angleSin}°): {result}");
                break;
            case "2":
                double angleCos = GetValidNumber("Skriv vinkel:");
                Console.Write("Er vinkelen i grader? (ja/nei, standard ja): ");
                var cosDeg = Console.ReadLine()?.Trim().ToLower() != "nei";
                result = _calculator.Cos(angleCos, cosDeg);
                Console.WriteLine($"Cos({angleCos}°): {result}");
                break;
            case "3":
                double angleTan = GetValidNumber("Skriv vinkel:");
                Console.Write("Er vinkelen i grader? (ja/nei, standard ja): ");
                var tanDeg = Console.ReadLine()?.Trim().ToLower() != "nei";
                result = _calculator.Tan(angleTan, tanDeg);
                Console.WriteLine($"Tan({angleTan}°): {result}");
                break;
            case "4":
                double sqrtVal = GetValidNumber("Skriv tall:");
                result = _calculator.Sqrt(sqrtVal);
                if (!double.IsNaN(result))
                    Console.WriteLine($"√{sqrtVal}: {result}");
                break;
            case "5":
                double baseVal = GetValidNumber("Skriv base:");
                double expVal = GetValidNumber("Skriv eksponent:");
                result = _calculator.Power(baseVal, expVal);
                Console.WriteLine($"{baseVal}^{expVal}: {result}");
                break;
            case "6":
                double lnVal = GetValidNumber("Skriv tall:");
                result = _calculator.Ln(lnVal);
                if (!double.IsNaN(result))
                    Console.WriteLine($"Ln({lnVal}): {result}");
                break;
            case "7":
                result = _calculator.E;
                Console.WriteLine($"e: {result}");
                break;
            case "8":
                result = _calculator.Pi;
                Console.WriteLine($"π: {result}");
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ugyldig valg.");
                Console.ResetColor();
                valid = false;
                break;
        }

        if (valid && !double.IsNaN(result))
        {
            Console.Write("Vil du kryptere resultatet? (ja/nei): ");
            var encryptChoice = Console.ReadLine()?.Trim().ToLower();
            if (encryptChoice == "ja" || encryptChoice == "j")
            {
                string encrypted = EncryptionHelper.Encrypt(result.ToString());
                Console.WriteLine($"Kryptert resultat: {encrypted}");
            }
        }

        Console.Write("Vil du gjøre en ny avansert beregning? (ja/nei): ");
        var continueInput = Console.ReadLine()?.Trim().ToLower();
        if (continueInput != "ja" && continueInput != "j")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tilbake til hovedmeny...");
            Console.ResetColor();
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