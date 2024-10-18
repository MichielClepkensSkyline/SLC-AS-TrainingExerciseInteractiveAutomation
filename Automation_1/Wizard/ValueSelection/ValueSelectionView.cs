namespace Automation_1.Wizard.ValueSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ValueSelectionView : Dialog
	{
		public ValueSelectionView(IEngine engine) : base(engine)
		{
			Title = "Enter new parameter value";

			Input = new TextBox
			{
				Width = 310,
				IsMultiline = true,
			};
			BackButton = new Button("Back");
			ExitButton = new Button("Exit");
			FinishButton = new Button("Finish");

			AddWidget(new Label("Value:"), 0, 0);
			AddWidget(Input, 1, 0, 1, 3);
			AddWidget(BackButton, 2, 0);
			AddWidget(ExitButton, 2, 1);
			AddWidget(FinishButton, 2, 2);
		}

		public TextBox Input { get; set; }

		public Button BackButton { get; }

		public Button ExitButton { get; }

		public Button FinishButton { get; }
	}
}
