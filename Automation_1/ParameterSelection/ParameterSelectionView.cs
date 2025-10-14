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

	public class ParameterSelectionView : Dialog
	{
		public ParameterSelectionView(IEngine engine) : base(engine)
		{

			ContinueButton = new Button("Continue...");
			BackButton = new Button("Back");
			ParameterId = new Numeric();
			ParameterId.Minimum = 0;

			AddWidget(ParameterId, 0, 0);
			AddWidget(BackButton, 1, 0);
			AddWidget(ContinueButton, 1, 1);
		}

		public Numeric ParameterId { get; }

		public Button ContinueButton { get; }

		public Button BackButton { get; }
	}
}
