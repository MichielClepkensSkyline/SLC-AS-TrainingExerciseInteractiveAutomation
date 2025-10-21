namespace Automation_1.ParameterSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ParameterSelectionView : Dialog, IParameterSelectionView
	{
		public ParameterSelectionView(IEngine engine) : base(engine)
		{
			Title = "Parameter Set";
			ContinueButton = new Button("Continue...");
			BackButton = new Button("Back");
			ParameterIdDropDown = new DropDown() { IsSorted = true, IsDisplayFilterShown = true };
			SelectParameterLabel = new Label("Select a parameter: ");

			AddWidget(SelectParameterLabel, 0, 0);
			AddWidget(ParameterIdDropDown, 0, 1);
			AddWidget(BackButton, 1, 0);
			AddWidget(ContinueButton, 1, 1);
		}

		public DropDown ParameterIdDropDown { get; }

		public Button ContinueButton { get; }

		public Button BackButton { get; }

		public Label SelectParameterLabel { get; private set; }
	}
}
