namespace Automation_1.ElementSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IElementSelectionView
	{
		Label SetParameterLabel { get; set; }

		DropDown ElementsDropDown { get; }

		Button ContinueButton { get; }
	}
}
