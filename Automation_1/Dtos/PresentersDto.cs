namespace Automation_1.Dtos
{
	using Automation_1.Wizard.ElementSelection;
	using Automation_1.Wizard.ParameterSelection;
	using Automation_1.Wizard.ValueSelection;

	public class PresenterDto
	{
		public ElementSelectionPresenter ElementSelection { get; set; }

		public ParameterSelectionPresenter ParameterSelection { get; set; }

		public ValueSelectionPresenter ValueSelection { get; set; }
	}
}
