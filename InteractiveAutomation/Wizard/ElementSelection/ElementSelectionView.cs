using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Core.DataMinerSystem.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAutomation.Wizard.ElementSelection
{
	internal class ElementSelectionView : Dialog, IElementSelectionView
	{
		private readonly Label titleLabel;
		private readonly Label elementLabel;

		public ElementSelectionView(IEngine engine) : base(engine)
		{
			// Alles wat op de UI komt aanmaken (komt allemaal uit de INteractiveAutomationScript package)
			titleLabel = new Label($"This is supposed to be the title");
			elementLabel = new Label($"Element: ");

			var dms = engine.GetDms(); // Hier nog safety voor voorzien
			ElementsDropDown = new DropDown(dms.GetElements().Select(element => element.Name)) { IsDisplayFilterShown = true, IsSorted = true };
			NextButton = new Button($"Next");

			Title = "This is the title";

			// Toevoegen van de UI components aan een bepaalde plek in de UI
			AddWidget(titleLabel, 0, 0, 1, 2);
			AddWidget(elementLabel, 1, 0);
			AddWidget((DropDown)ElementsDropDown, 1, 1);
			AddWidget(NextButton, 2, 1);
		}

		public IDropDown ElementsDropDown { get; private set; }

		public Button NextButton { get; private set; }

		// public Label ValidationLabel { get; private set; }
	}
}
