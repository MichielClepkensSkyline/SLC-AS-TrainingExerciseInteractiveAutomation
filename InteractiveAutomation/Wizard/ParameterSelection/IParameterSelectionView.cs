namespace InteractiveAutomation.Wizard.ParameterSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal interface IParameterSelectionView
	{
		IDropDown ParametersDropDown { get; }

		Button NextButton { get; }

		Button BackButton { get; }
	}
}
