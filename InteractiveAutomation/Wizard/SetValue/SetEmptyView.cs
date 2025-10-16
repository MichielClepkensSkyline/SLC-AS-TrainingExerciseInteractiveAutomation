namespace InteractiveAutomation.Wizard.SetValue
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class SetEmptyView : Dialog , ISetValueView
	{
		private readonly Label setLabel;

		public SetEmptyView(IEngine engine) : base(engine)
		{
			setLabel = new Label($"Value: ");
			ValueBox = new TextBox() { Width = 150, IsEnabled = false };
			SetButton = new Button($"Set Value");

			MessageBox = new TextBox() { IsReadOnly = true, IsMultiline = true, IsEnabled = false };

			BackButton = new Button($"Back...");
			FinishButton = new Button($"Exit");

			Title = $"Set value";

			AddWidget(setLabel, 0, 0);
			AddWidget(ValueBox, 0, 1);
			AddWidget(SetButton, 0, 2);
			AddWidget(MessageBox, 2, 0, 1, 3);
			AddWidget(BackButton, 3, 0);
			AddWidget(FinishButton, 3, 1);
		}

		public TextBox ValueBox { get; }

		public Button SetButton { get; }

		public TextBox MessageBox { get; }

		public Button FinishButton { get; }

		public Button BackButton { get; }
	}
}
