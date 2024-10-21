namespace Automation_1.Wizard.ValueSelection
{
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IValueSelectionView
	{
		Button BackButton { get; }

		Button ExitButton { get; }

		Button FinishButton { get; }

		Widget CurrentInput { get; set; }

		void SetFeedbackMessage(string message);
	}
}
