using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Core.DataMinerSystem.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAutomation.Wizard.ParameterSelection
{
	internal class ParameterSelectionView : Dialog, IParameterSelectionView
	{
		private readonly Label titleLabel;
		private readonly Label elementLabel;

		public ParameterSelectionView(IEngine engine) : base(engine)
		{
			// Alles wat op de UI komt aanmaken (komt allemaal uit de INteractiveAutomationScript package)
			elementLabel = new Label($"Parameter: ");

			var dms = engine.GetDms(); // Hier nog safety voor voorzien
			ElementsDropDown = new DropDown(dms.GetElements().Select(element => element.Name)) { IsDisplayFilterShown = true, IsSorted = true };
			NextButton = new Button($"Next");

			Title = $"Select your parameter:";

			// Toevoegen van de UI components aan een bepaalde plek in de UI
			AddWidget(elementLabel, 1, 0);
			AddWidget((DropDown)ElementsDropDown, 1, 1);
			AddWidget(NextButton, 2, 1);
		}

		public Button NextButton { get; private set; }

		public IDropDown ElementsDropDown { get; }
	}
}
