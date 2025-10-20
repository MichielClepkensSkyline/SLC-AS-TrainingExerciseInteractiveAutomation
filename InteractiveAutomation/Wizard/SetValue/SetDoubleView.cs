namespace InteractiveAutomation.Wizard.SetValue
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class SetDoubleView : ASetValueView, ISetValueView
	{
		public SetDoubleView(IEngine engine) : base(engine)
		{
			ValueBox = new Numeric() { Decimals = 2, StepSize = 0.01 };
			SetButton = new Button($"Set Value");

			MessageBox = new TextBox() { IsReadOnly = true, IsMultiline = true, IsEnabled = false };

			BackButton = new Button($"Back...");
			FinishButton = new Button($"Exit");

			Title = $"Set value";

			AddWidget(ValueBox, 0, 1);
			AddWidget(SetButton, 0, 2);
			AddWidget(MessageBox, 2, 0, 1, 3);
			AddWidget(BackButton, 3, 0);
			AddWidget(FinishButton, 3, 1);
		}

		public Numeric ValueBox { get; }

		public Button SetButton { get; }

		public TextBox MessageBox { get; }

		public Button FinishButton { get; }

		public Button BackButton { get; }
	}
}
