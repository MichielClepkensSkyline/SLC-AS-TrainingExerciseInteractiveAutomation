namespace Automation_1.Wizard.ElementSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ElementSelectionView : Dialog, IElementSelectionView
	{
		public ElementSelectionView(IEngine engine) : base(engine)
		{
			Title = "Select Target Element for Parameter Setup";
			Height = 500;
			MinHeight = 500;
			Width = 500;
			MinWidth = 500;

			ElementsDropDown = new DropDown();

			AddWidget(new Label("Select an element:"), 0, 0);
			AddWidget(ElementsDropDown, 0, 1);
		}

		public DropDown ElementsDropDown { get; }
	}
}
