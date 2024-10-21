namespace Automation_1.Wizard.ElementSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ElementSelectionView : Dialog, IElementSelectionView
	{
		public ElementSelectionView(IEngine engine) : base(engine)
		{
			Title = "Select Target Element for Parameter Setup";

			ElementsDropDown = new DropDown
			{
				IsSorted = true,
				IsDisplayFilterShown = true,
			};
			ContinueButton = new Button("Continue");

			AddWidget(new Label("Select an element:"), 0, 0);
			AddWidget(ElementsDropDown, 0, 1);
			AddWidget(ContinueButton, 1, 1);
		}

		public DropDown ElementsDropDown { get; }

		public Button ContinueButton { get; }
	}
}
