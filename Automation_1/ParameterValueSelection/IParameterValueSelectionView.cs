namespace Automation_1.ParameterValueSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IParameterValueSelectionView
	{
		Button ExitButton { get; }

		Button BackButton { get; }

		TextBox StringValue { get; }

		Numeric DoubleValue { get; }

		Button SetStringValue { get; }

		Button SetDoubleValue { get; }

		TextBox Message { get; }

		Label StringValueLabel { get; }

		Label DoubleValueLabel { get; }
	}
}
