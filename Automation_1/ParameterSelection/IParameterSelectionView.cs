namespace Automation_1.ParameterSelection
{
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public interface IParameterSelectionView
    {
        IDropDown ParameterIdDropDown { get; }

        Button NextButton { get; }

        Button BackButton { get; }
    }
}
