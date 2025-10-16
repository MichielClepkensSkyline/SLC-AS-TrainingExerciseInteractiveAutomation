namespace InteractiveAutomation.Wizard.SetValue
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal interface ISetValueView
	{
		Button SetButton { get; }

		TextBox MessageBox { get; }

		Button FinishButton { get; }

		Button BackButton { get; }
	}
}
