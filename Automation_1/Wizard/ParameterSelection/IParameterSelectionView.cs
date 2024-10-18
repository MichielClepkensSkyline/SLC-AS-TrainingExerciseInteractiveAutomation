namespace Automation_1.Wizard.ParameterSelection
{
	using System.Collections.Generic;

	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IParameterSelectionView
	{
		DropDown ParametersDropDown { get; }

		Button BackButton { get; }

		Button ContinueButton { get; }
	}
}
