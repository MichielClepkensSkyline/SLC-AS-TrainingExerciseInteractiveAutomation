namespace Automation_1.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ElementSelectionView : Dialog
	{
		public ElementSelectionView(IEngine engine) : base(engine)
		{
			ElementsDropDown = new DropDown { IsSorted = true, IsDisplayFilterShown = true };
			ContinueButton = new Button("Continue...");
			Title = "Pick an element";
			ElementLabel = new Label("Element");
			AddWidget(ElementLabel, 0, 0);
			AddWidget(ElementsDropDown, 0, 1);
			AddWidget(ContinueButton, 1, 1);
		}

		public DropDown ElementsDropDown { get; }

		public Button ContinueButton { get; }

		public Label ElementLabel { get; set; }
	}
}
