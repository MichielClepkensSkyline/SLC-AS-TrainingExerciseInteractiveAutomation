namespace Automation_1.Wizard.ValueSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IValueSelectionView
	{
		TextBox Input { get; set; }

		Button BackButton { get; }

		Button ExitButton { get; }

		Button FinishButton { get; }

		void SetFeedbackMessage (string message);
	}
}
