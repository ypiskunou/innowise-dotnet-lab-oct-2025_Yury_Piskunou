namespace Calculator.Operations;

public class Addition: IOperation
{
    public char Operator => '+';
    public double Execute(double leftOperand, double rightOperand)
    {
        return leftOperand + rightOperand;
    }
}