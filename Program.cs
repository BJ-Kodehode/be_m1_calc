using be_m1_calc.Services;

namespace be_m1_calc;

class Program
{
    static void Main(string[] args)
    {
        var calculatorService = new CalculatorService();
        calculatorService.RunCalculator();
    }
}
