namespace InteractiveAutomation.Wizard.ParameterSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	internal class ParameterSelectionPresenter
	{
		private readonly IParameterSelectionView parameterSelectionView;

		public ParameterSelectionPresenter(IParameterSelectionView view)
		{
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(view));

			parameterSelectionView.NextButton.Pressed += OnNextPressed;
			parameterSelectionView.BackButton.Pressed += OnBackPressed;
		}

		public event EventHandler<EventArgs> Next;

		public event EventHandler<EventArgs> Back;

		private void OnNextPressed(object sender, EventArgs e)
		{
			// StoreToModel
			Next?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackPressed(object sender, EventArgs e)
		{
			// StoreToModel
			Back?.Invoke(this, EventArgs.Empty);
		}
	}
}
