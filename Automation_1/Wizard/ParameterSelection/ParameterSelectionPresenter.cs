namespace Automation_1.Wizard.ParameterSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Automation_1.Dtos;

	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ParameterSelectionPresenter
	{
		private readonly IParameterSelectionView _view;
		private readonly IParameterSetter _model;

		private Dictionary<string, ParameterInfo> _parametersByName;

		public ParameterSelectionPresenter(IParameterSelectionView view, IParameterSetter model)
		{
			_view = view ?? throw new ArgumentNullException(nameof(view));
			_model = model ?? throw new ArgumentNullException(nameof(model));

			_view.BackButton.Pressed += OnBackButtonPressed;
			//_view.ContinueButton.Pressed += OnContinueButtonPressed;
		}

		public event EventHandler<EventArgs> Back;

		public event EventHandler<EventArgs> Continue;

		public void LoadFromModel()
		{
			_parametersByName = _model.Parameters.ToDictionary(parameter => $"{parameter.Name} / {parameter.Id}");

			_view.ParametersDropDown.SetOptions(_parametersByName.Keys);
			_view.ParametersDropDown.Selected = _model.SelectedParameter?.Name ?? string.Empty;
		}

		private void StoreToModel()
		{
			string selected = _view.ParametersDropDown.Selected;
			_model.SelectedParameter = _parametersByName[selected];
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Back?.Invoke(this, EventArgs.Empty);
		}
	}
}
