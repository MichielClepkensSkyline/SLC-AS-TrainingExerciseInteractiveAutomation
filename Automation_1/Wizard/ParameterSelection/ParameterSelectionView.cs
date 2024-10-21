namespace Automation_1.Wizard.ParameterSelection
{
	using System.Collections.Generic;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ParameterSelectionView : Dialog, IParameterSelectionView
	{
		public ParameterSelectionView(IEngine engine) : base(engine)
		{
			Title = "Select Target Parameter";

			ParametersDropDown = new DropDown
			{
				IsSorted = true,
				IsDisplayFilterShown = true,
			};
			BackButton = new Button("Back");
			ContinueButton = new Button("Continue");

			AddWidget(new Label("Select a parameter:"), 0, 0);
			AddWidget(ParametersDropDown, 0, 1);
			AddWidget(BackButton, 1, 0);
			AddWidget(ContinueButton, 1, 1);
		}

		public DropDown ParametersDropDown { get; }

		public Button BackButton { get; }

		public Button ContinueButton { get; }

		public string SelectedParameter
		{
			get => ParametersDropDown.Selected;
			set => ParametersDropDown.Selected = value;
		}

		public void SetOptions(IEnumerable<string> options)
		{
			ParametersDropDown.SetOptions(options);
		}
	}
}
