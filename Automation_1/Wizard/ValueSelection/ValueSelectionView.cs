namespace Automation_1.Wizard.ValueSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ValueSelectionView : Dialog, IValueSelectionView
	{
		public ValueSelectionView(IEngine engine) : base(engine)
		{
			Title = "Set new parameter value";

			Input = new TextBox
			{
				PlaceHolder = "Enter new parameter value",
				Width = 310,
				IsMultiline = true,
			};
			BackButton = new Button("Back");
			ExitButton = new Button("Exit");
			FinishButton = new Button("Finish");
			Feedback = new TextBox
			{
				Width = 310,
				IsMultiline = true,
				IsEnabled = false,
				MinHeight = 50,
			};

			AddWidget(new Label("Value:"), 0, 0);
			AddWidget(Input, 1, 0, 1, 3);
			AddWidget(new Label("Feedback:"), 2, 0);
			AddWidget(Feedback, 3, 0, 1, 3);
			AddWidget(BackButton, 4, 0);
			AddWidget(ExitButton, 4, 1);
			AddWidget(FinishButton, 4, 2);
		}

		public TextBox Input { get; set; }

		public Button BackButton { get; }

		public Button ExitButton { get; }

		public Button FinishButton { get; }

		public TextBox Feedback { get; set; }

		public void SetFeedbackMessage(string message)
		{
			Feedback.Text = message;
		}
	}
}
