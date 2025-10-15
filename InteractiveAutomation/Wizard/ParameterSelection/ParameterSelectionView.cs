namespace InteractiveAutomation.Wizard.ParameterSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Net.Messages;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class ParameterSelectionView : Dialog, IParameterSelectionView
	{
		// private readonly Label titleLabel;
		private readonly Label parameterLabel;

		public ParameterSelectionView(IEngine engine) : base(engine)
		{
			// Alles wat op de UI komt aanmaken (komt allemaal uit de INteractiveAutomationScript package)
			parameterLabel = new Label($"Parameter ID: ");
			ParametersDropDown = new DropDown();

			NextButton = new Button($"Continue...");
			BackButton = new Button($"Back...");

			Title = $"Select parameter";

			// Toevoegen van de UI components aan een bepaalde plek in de UI
			AddWidget(parameterLabel, 0, 0);
			AddWidget((DropDown)ParametersDropDown, 0, 1);
			AddWidget(BackButton, 1, 0);
			AddWidget(NextButton, 1, 1);
		}

		public IDropDown ParametersDropDown { get; }

		public Button NextButton { get; }

		public Button BackButton { get; }
	}
}
