namespace InteractiveAutomation.Wizard.ParameterSelection
{
	using System;
	using System.Linq;
	using Skyline.DataMiner.Automation;

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

		public void LoadFromModel()
		{
			if (model.Parameters != null && model.Parameters.Any())
			{
				parameterSelectionView.ParametersDropDown.SetOptions(model.Parameters.Select(x => $"{x.Key} {x.Value.Name}"));
			}
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
			string[] parameterinfo = parameterSelectionView.ParametersDropDown.Selected.Split(' ');
			int parameterid = 0;
			if (parameterinfo.Length > 0 && !String.IsNullOrWhiteSpace(parameterinfo[0]))
			{
				parameterid = Convert.ToInt32(parameterinfo[0]);
			}

			model.SelectedParameterId = parameterid;
		}
	}
}
