namespace InteractiveAutomation.Wizard.ElementSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class ElementSelectionView : Dialog, IElementSelectionView
	{
		private readonly Label elementLabel;

		public ElementSelectionView(IEngine engine) : base(engine)
		{
			elementLabel = new Label($"Element: ");

			ElementsDropDown = new DropDown() { IsDisplayFilterShown = true, IsSorted = true };
			NextButton = new Button($"Continue...");

			Title = "Select element";

			AddWidget(elementLabel, 0, 0);
			AddWidget((DropDown)ElementsDropDown, 0, 1);
			AddWidget(NextButton, 1, 1);
		}

		public IDropDown ElementsDropDown { get; }

		public Button NextButton { get; }
	}
}
