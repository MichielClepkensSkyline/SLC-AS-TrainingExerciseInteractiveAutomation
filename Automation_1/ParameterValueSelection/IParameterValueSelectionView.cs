namespace Automation_1.ParameterValueSelection
{
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public interface IParameterValueSelectionView
    {
        Label StringValueLabel { get; }

        Label DoubleValueLabel { get; }

        TextBox StringValueTextBox { get; }

        Button SetStringVauleButton { get; }

        Numeric DoubleValueNumeric { get; }

        Button SetDoubleValueButton { get; }

        TextBox ExceptionTextBox { get; }

        Button BackButton { get; }

        Button ExitButton { get; }
    }
}
