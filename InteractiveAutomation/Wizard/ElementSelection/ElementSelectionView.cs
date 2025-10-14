namespace InteractiveAutomation.Wizard.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Permissions;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class ElementSelectionView : Dialog, IElementSelectionView
	{
		private readonly Label elementLabel;

		public ElementSelectionView(IEngine engine) : base(engine)
		{
			// Alles wat op de UI komt aanmaken (komt allemaal uit de INteractiveAutomationScript package)
			elementLabel = new Label($"Element: ");

			ElementsDropDown = new DropDown() { IsDisplayFilterShown = true, IsSorted = true };
			NextButton = new Button($"Continue...");

			Title = "Select element";

			// Toevoegen van de UI components aan een bepaalde plek in de UI
			AddWidget(elementLabel, 0, 0);
			AddWidget((DropDown)ElementsDropDown, 0, 1);
			AddWidget(NextButton, 1, 1);
		}

		public IDropDown ElementsDropDown { get; }

		public Button NextButton { get; }
	}
}
