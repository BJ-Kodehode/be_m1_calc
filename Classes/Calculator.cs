using System;
using be_m1_calc;

namespace be_m1_calc.Classes;

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

    public double Sin(double angle, bool isDegrees = true)
    {
        double radians = isDegrees ? angle * Math.PI / 180 : angle;
        return Math.Sin(radians);
    }

    public double Cos(double angle, bool isDegrees = true)
    {
        double radians = isDegrees ? angle * Math.PI / 180 : angle;
        return Math.Cos(radians);
    }

    public double Tan(double angle, bool isDegrees = true)
    {
        double radians = isDegrees ? angle * Math.PI / 180 : angle;
        return Math.Tan(radians);
    }

    public double Sqrt(double value)
    {
        if (value < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Feil: Kvadratrot av negativt tall er ikke tillatt.");
            Console.ResetColor();
            return double.NaN;
        }
        return Math.Sqrt(value);
    }

    public double Power(double baseValue, double exponent)
    {
        return Math.Pow(baseValue, exponent);
    }

    public double Ln(double value)
    {
        if (value <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Feil: Naturlig logaritme av ikke-positivt tall er ikke tillatt.");
            Console.ResetColor();
            return double.NaN;
        }
        return Math.Log(value);
    }

    public double E => Math.E;

    public double Pi => Math.PI;
}