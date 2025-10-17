namespace Automation_1.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ElementSelectionView : Dialog, IElementSelectionView
	{
		public ElementSelectionView(IEngine engine) : base(engine)
		{
			ElementsDropDown = new DropDown { IsSorted = true, IsDisplayFilterShown = true };
			ContinueButton = new Button("Continue...") { Width = 150 };
			SetParameterLabel = new Label("Select an element: ");
			Title = "Element Set";
			AddWidget(SetParameterLabel, 0, 0);
			AddWidget(ElementsDropDown, 0, 1);
			AddWidget(ContinueButton, 1, 1);
		}

		public Label SetParameterLabel { get; set; }

		public DropDown ElementsDropDown { get; }

		public Button ContinueButton { get; }
	}
}
