namespace Automation_1.ElementSelection
{
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public class ElementSelectionView : Dialog, IElementSelectionView
    {
        public ElementSelectionView(IEngine engine) : base(engine)
        {
            ElementDropDown = new DropDown { IsSorted = true, IsDisplayFilterShown = true };
            NextButton = new Button("Next");

            AddWidget(new Label("Element"), 0, 0);
            AddWidget((Widget)ElementDropDown, 0, 1);
            AddWidget(NextButton, 1, 1);
        }

        public Button NextButton { get; }

        public IDropDown ElementDropDown { get; }
    }
}
