namespace Calculator;

public interface ICalculator
{
    double Calculate(double leftOperand, char operation, double rightOperand);
}