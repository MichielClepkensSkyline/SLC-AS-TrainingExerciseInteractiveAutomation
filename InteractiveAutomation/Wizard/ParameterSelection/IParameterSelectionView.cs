namespace InteractiveAutomation.Wizard.ParameterSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal interface IParameterSelectionView
	{
		IDropDown ParametersDropDown { get; }

		Button NextButton { get; }

		Button BackButton { get; }
	}
}
