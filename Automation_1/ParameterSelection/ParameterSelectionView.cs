namespace Automation_1.ParameterSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Messages;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ParameterSelectionView : Dialog, IParameterSelectionView
	{
		public ParameterSelectionView(IEngine engine) : base(engine)
		{
			Title = "Parameter Set";
			ContinueButton = new Button("Continue...");
			BackButton = new Button("Back");
			ParameterId = new Numeric();
			SelectParameterLabel = new Label("Select a parameter: ");
			ParameterId.Minimum = 0;

			AddWidget(SelectParameterLabel, 0, 0);
			AddWidget(ParameterId, 0, 1);
			AddWidget(BackButton, 1, 0);
			AddWidget(ContinueButton, 1, 1);
		}

		public Numeric ParameterId { get; }

		public Button ContinueButton { get; }

		public Button BackButton { get; }

		public Label SelectParameterLabel { get; }
	}
}
