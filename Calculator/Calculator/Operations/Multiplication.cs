namespace Calculator.Operations;

public class Multiplication: IOperation
{
    public char Operator => '*';

    public double Execute(double leftOperand, double rightOperand)
    {
        return leftOperand * rightOperand;
    }
}