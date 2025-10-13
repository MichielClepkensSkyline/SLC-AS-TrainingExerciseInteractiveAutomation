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
		TextBox StringValueBox { get; }

		Button StringButton { get; }

		Numeric DoubleValueBox { get; }

		Button DoubleButton { get; }

		TextBox MessageBox { get; }

		Button FinishButton { get; }

		Button BackButton { get; }
	}
}
