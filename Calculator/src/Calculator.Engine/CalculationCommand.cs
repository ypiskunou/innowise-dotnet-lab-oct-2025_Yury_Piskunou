namespace Calculator.Engine;

using Core;

public class CalculationCommand : ICommand
{
    private readonly double _newValue;
    private readonly double _previousValue;

    public CalculationCommand(double previousValue, double newValue)
    {
        _previousValue = previousValue;
        _newValue = newValue;
    }

    public double Execute()
    {
        return _newValue;
    }

    public double UnExecute()
    {
        return _previousValue;
    }
}