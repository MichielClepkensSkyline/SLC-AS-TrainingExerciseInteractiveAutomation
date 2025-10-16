namespace InteractiveAutomation.Wizard.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal interface IElementSelectionView
	{
		Button NextButton { get; }

		IDropDown ElementsDropDown { get; }
	}
}
