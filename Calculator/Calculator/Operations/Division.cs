namespace Calculator.Operations;

public class Division: IOperation
{
    public char Operator => '/';

    public double Execute(double leftOperand, double rightOperand)
    {
        if(rightOperand == 0) 
            throw new DivideByZeroException("Division by zero");
        return leftOperand / rightOperand;
    }
}