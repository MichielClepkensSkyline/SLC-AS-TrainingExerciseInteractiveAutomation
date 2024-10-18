namespace Automation_1.Wizard.ParameterSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Automation_1.Dtos;

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
			_view.ContinueButton.Pressed += OnContinueButtonPressed;
		}

		public event EventHandler<EventArgs> Back;

		public event EventHandler<EventArgs> Continue;

		public void LoadFromModel()
		{
			_parametersByName = _model.Parameters.ToDictionary(
				parameter => parameter.Name,
				parameter => parameter);
			_view.ParametersDropDown.SetOptions(_parametersByName.Keys);

			if (_model.SelectedParameter != null)
			{
				_view.ParametersDropDown.Selected = _model.SelectedParameter.Name;
			}
			else
			{
				_view.ParametersDropDown.Selected = string.Empty;
			}
		}

		private void StoreToModel()
		{
			string selectedName = _view.ParametersDropDown.Selected;

			if (_parametersByName.TryGetValue(selectedName, out var parameterInfo))
			{
				_model.SelectedParameter = parameterInfo;
			}
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Back?.Invoke(this, EventArgs.Empty);
		}

		private void OnContinueButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Continue?.Invoke(this, EventArgs.Empty);
		}
	}
}
