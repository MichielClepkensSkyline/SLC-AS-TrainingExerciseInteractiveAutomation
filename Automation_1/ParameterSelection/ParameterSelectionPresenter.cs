using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterSelection
{
    public class ParameterSelectionPresenter
    {
        private readonly IElementSelector _model;
        private readonly IParameterSelectionView _view;
        public bool ParameterExists;

        public ParameterSelectionPresenter(IElementSelector model, IParameterSelectionView view)
        {
            _model=model ?? throw new ArgumentNullException(nameof(view));
            _view=view ?? throw new ArgumentNullException(nameof(model));

            view.NextButton.Pressed += OnNextButtonPressed;
            view.BackButton.Pressed += OnBackButtonPressed;
        }

        public event EventHandler<EventArgs> Next;

        public event EventHandler<EventArgs> Back;

        private void OnNextButtonPressed(object sender, EventArgs e)
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

        private void OnBackButtonPressed(object sender, EventArgs e)
        {
            StoreToModel();
            Back?.Invoke(this, EventArgs.Empty);
        }

        private void StoreToModel()
        {
            int parameterId = (int)_view.ParameterId.Value;
            _model.SelectedParameter=parameterId;
        }
    }
}
