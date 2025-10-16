namespace InteractiveAutomation.Wizard.ElementSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal interface IElementSelectionView
	{
		Button NextButton { get; }

		IDropDown ElementsDropDown { get; }
	}
}
