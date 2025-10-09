namespace Calculator.Operations;

public class Subtraction: IOperation
{
    public char Operator => '-';

    public double Execute(double leftOperand, double rightOperand)
    {
        return leftOperand - rightOperand;
    }
}