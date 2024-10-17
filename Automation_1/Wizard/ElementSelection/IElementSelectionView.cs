namespace Automation_1.Wizard.ElementSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IElementSelectionView
	{
		DropDown ElementsDropDown { get; }

		Button ContinueButton { get; }
	}
}
