namespace be_m1_calc;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        while (true)
        {
            var userInput = Console.ReadLine();
            if (userInput != null &&
            userInput.Equals("exit", StringComparison.InvariantCultureIgnoreCase) ||
            userInput.Equals("quit", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Exiting program...");
                Console.ResetColor();
                break;
            }
        }
    }
}
