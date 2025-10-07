namespace Calculator.ConsoleApp;

using Core;
using Engine;

public class Program
{
    public static void Main(string[] args)
    {
        ICalculationService calculationService = new CalculationServiceWithRegex();
        
        ICalculatorSession session = new CalculatorEngine(calculationService);
        
        IApplicationHost host = new ConsoleUI(session);
        
        
        host.Run();
    }
}