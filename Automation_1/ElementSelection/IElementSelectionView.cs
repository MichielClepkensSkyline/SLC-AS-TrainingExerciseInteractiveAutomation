namespace Automation_1.ElementSelection
{
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public interface IElementSelectionView
    {
        Button NextButton { get; }

        IDropDown ElementDropDown { get; }
    }
}
