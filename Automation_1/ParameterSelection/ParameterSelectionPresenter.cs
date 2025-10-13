namespace Automation_1.ParameterSelection
{
	using Automation_1.ElementSelection;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ParameterSelectionPresenter
	{
		private readonly ParameterSelectionView parameterSelectionView;
		private readonly IElementSelector elementSelector;

		public ParameterSelectionPresenter(ParameterSelectionView view, IElementSelector element)
		{
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(parameterSelectionView));
			elementSelector = element ?? throw new ArgumentNullException(nameof(elementSelector));

			parameterSelectionView.ContinueButton.Pressed += OnContinueButtonPressed;

			parameterSelectionView.BackButton.Pressed += OnBackButtonPressed;
		}

		public event EventHandler<EventArgs> Continue;

		public event EventHandler<EventArgs> Back;


		private void OnContinueButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();

			Continue?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();

			Back?.Invoke(this, EventArgs.Empty);
		}

		private void StoreToModel()
		{
			elementSelector.SelectedParameterId = (int)parameterSelectionView.ParameterId.Value;
		}
	}
}
