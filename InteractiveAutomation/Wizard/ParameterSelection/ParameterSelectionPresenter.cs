namespace InteractiveAutomation.Wizard.ParameterSelection
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	internal class ParameterSelectionPresenter
	{
		private readonly IParameterSelectionView parameterSelectionView;
		private readonly IModel model;
		private readonly IEngine engine;

		public ParameterSelectionPresenter(IParameterSelectionView view, IModel model, IEngine engine)
		{
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));
			this.engine = engine;

			parameterSelectionView.ParameterValue.FocusLost += OnFocusLost;

			parameterSelectionView.NextButton.Pressed += OnNextPressed;
			parameterSelectionView.BackButton.Pressed += OnBackPressed;
		}

		// public event EventHandler<Numeric.NumericFocusLostEventArgs> Print;

		public event EventHandler<EventArgs> Next;

		public event EventHandler<EventArgs> Back;

		private void OnFocusLost(object sender, Numeric.NumericFocusLostEventArgs e)
		{
			engine.Log("[DEBUG] into onfocuslost");
		}

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
