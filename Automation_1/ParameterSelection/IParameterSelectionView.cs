namespace Automation_1.ParameterSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IParameterSelectionView
	{
		DropDown ParameterIdDropDown { get; }

		Button ContinueButton { get; }

		Button BackButton { get; }

		Label SelectParameterLabel { get; }
	}
}
