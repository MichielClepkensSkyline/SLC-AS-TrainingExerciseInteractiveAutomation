namespace Automation_1.ParameterSelection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Skyline.DataMiner.Net.Messages;

    public class ParameterSelectionPresenter
    {
        public Dictionary<string, ParameterInfo> parametersByName;
        public bool ParameterExists;
        private readonly IElementSelector _model;
        private readonly IParameterSelectionView _view;

        public ParameterSelectionPresenter(IElementSelector model, IParameterSelectionView view)
        {
            _model=model ?? throw new ArgumentNullException(nameof(view));
            _view=view ?? throw new ArgumentNullException(nameof(model));

            view.NextButton.Pressed += OnNextButtonPressed;
            view.BackButton.Pressed += OnBackButtonPressed;
        }

        public event EventHandler<EventArgs> Next;

        public event EventHandler<EventArgs> Back;

        public void OnNextButtonPressed(object sender, EventArgs e)
        {
            StoreToModel();
            try
            {
                var parameter = _model.SelectedElement.GetStandaloneParameter<string>(_model.SelectedParameter);
                var parameterValue = parameter.GetValue();
                ParameterExists = true;
            }
            catch
            {
                ParameterExists = false;
            }

            Next?.Invoke(this, EventArgs.Empty);
        }

        public void OnBackButtonPressed(object sender, EventArgs e)
        {
            StoreToModel();
            Back?.Invoke(this, EventArgs.Empty);
        }

        public void StoreToModel()
        {
            string selected = _view.ParameterIdDropDown.Selected;
            _model.SelectedParameter = parametersByName[selected].ID;
        }

        public void LoadFromModel()
        {
            if (_model.Elements == null || !_model.Elements.Any())
            {
                _view.ParameterIdDropDown.SetOptions(new List<string>());
                return;
            }

            if (_model.Parameters == null || !_model.Parameters.Any())
            {
                _view.ParameterIdDropDown.SetOptions(new List<string>());
                return;
            }

            parametersByName = _model.Parameters.ToDictionary(parameter => $"{parameter.Name} ({parameter.ID.ToString()})");

            _view.ParameterIdDropDown.SetOptions(parametersByName.Keys);
            _view.ParameterIdDropDown.Selected = _model.SelectedParameter.ToString();
        }
    }
}
