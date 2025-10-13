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
		private readonly IModel model;

		public ParameterSelectionPresenter(IParameterSelectionView view, IModel model)
		{
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));

			parameterSelectionView.NextButton.Pressed += OnNextPressed;
			parameterSelectionView.BackButton.Pressed += OnBackPressed;
		}

		public event EventHandler<EventArgs> Next;

		public event EventHandler<EventArgs> Back;

		private void OnNextPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Next?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Back?.Invoke(this, EventArgs.Empty);
		}

		private void StoreToModel()
		{
			int parameterid = (int)parameterSelectionView.ParameterValue.Value;
			model.SelectedParameterId = parameterid;
		}
	}
}
