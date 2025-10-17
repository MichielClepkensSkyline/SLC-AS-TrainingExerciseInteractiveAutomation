namespace Automation_1.ParameterValueSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ParameterValueSelectionView : Dialog, IParameterValueSelectionView
	{
		public ParameterValueSelectionView(IEngine engine) : base(engine)
		{
			Title = "Parameter Value Set";
			BackButton = new Button("Back");
			SetStringValue = new Button("Set String Value");
			SetDoubleValue = new Button("Set Double Value");
			StringValue = new TextBox() { Width = 100};
			DoubleValue = new Numeric() { Width = 100, Decimals = 2, StepSize= 0.01 };
			Message = new TextBox() { IsMultiline = true, IsEnabled = false };
			StringValueLabel = new Label("String Value");
			DoubleValueLabel = new Label("Double Value");

			AddWidget(StringValueLabel, 0, 0);
			AddWidget(StringValue, 0, 1);
			AddWidget(SetStringValue, 0, 2);
			AddWidget(DoubleValueLabel, 1, 0);
			AddWidget(DoubleValue, 1, 1);
			AddWidget(SetDoubleValue, 1, 2);
			AddWidget(BackButton, 2, 0);
			AddWidget(Message, 3, 0, 1, 3);
		}

		public Button BackButton { get; }

		public TextBox StringValue { get; }

		public Numeric DoubleValue { get; }

		public Button SetStringValue { get; }

		public Button SetDoubleValue { get; }

		public TextBox Message { get; set; }

		public Label StringValueLabel { get; }

		public Label DoubleValueLabel { get; }
	}
}
