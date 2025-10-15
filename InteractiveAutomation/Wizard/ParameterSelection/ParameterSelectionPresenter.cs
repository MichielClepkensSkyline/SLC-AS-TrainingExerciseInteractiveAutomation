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

			parameterSelectionView.NextButton.Pressed += OnNextPressed;
			parameterSelectionView.BackButton.Pressed += OnBackPressed;
		}

		public event EventHandler<EventArgs> Next;

		public event EventHandler<EventArgs> Back;

		public void LoadFromModel()
		{
			parameterSelectionView.ParametersDropDown.SetOptions(model.Parameters.Select(x => $"{x.Key} {x.Value}"));
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
			int parameterid = Convert.ToInt32(parameterSelectionView.ParametersDropDown.Selected.Split(' ')[0]);
			model.SelectedParameterId = parameterid;
		}
	}
}
