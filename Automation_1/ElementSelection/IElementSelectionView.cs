namespace Automation_1.ElementSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IElementSelectionView
	{
		Label SetParameterLabel { get; }

		DropDown ElementsDropDown { get; }

		Button ContinueButton { get; }
	}
}
