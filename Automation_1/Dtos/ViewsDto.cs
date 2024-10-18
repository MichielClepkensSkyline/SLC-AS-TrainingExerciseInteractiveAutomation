namespace Automation_1.Dtos
{
	using Automation_1.Wizard.ElementSelection;
	using Automation_1.Wizard.ParameterSelection;
	using Automation_1.Wizard.ValueSelection;

	public class ViewDto
	{
		public ElementSelectionView ElementSelectionView { get; set; }

		public ParameterSelectionView ParameterSelectionView { get; set; }

		public ValueSelectionView ValueSelectionView { get; set; }
	}
}
