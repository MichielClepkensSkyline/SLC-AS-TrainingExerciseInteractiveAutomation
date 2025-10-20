namespace InteractiveAutomation.Wizard.SetValue
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal abstract class ASetValueView : Dialog
	{
		private readonly Label elementLabel;

		protected ASetValueView(IEngine engine) : base(engine)
		{
			elementLabel = new Label("Value: ");
			AddWidget(elementLabel, 0, 0);
		}
	}
}
