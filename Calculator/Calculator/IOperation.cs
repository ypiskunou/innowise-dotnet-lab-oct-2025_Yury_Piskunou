namespace Calculator;

public interface IOperation
{
    char Operator { get; }
    double Execute(double leftOperand, double rightOperand);
}