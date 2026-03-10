namespace be_m1_calc;

public interface ICalculator
{
    double AddNumbers(double a, double b);
    double SubtractNumbers(double a, double b);
    double MultiplyNumbers(double a, double b);
    double DivideNumbers(double a, double b);
    double Sin(double angle, bool isDegrees = true);
    double Cos(double angle, bool isDegrees = true);
    double Tan(double angle, bool isDegrees = true);
    double Sqrt(double value);
    double Power(double baseValue, double exponent);
    double Ln(double value);
    double E { get; }
    double Pi { get; }
}