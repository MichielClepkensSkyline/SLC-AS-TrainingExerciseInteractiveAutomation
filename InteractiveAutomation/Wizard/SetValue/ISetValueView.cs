namespace InteractiveAutomation.Wizard.SetValue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal interface ISetValueView
	{
		Button FinishButton { get; }

		Button BackButton { get; }
	}
}
