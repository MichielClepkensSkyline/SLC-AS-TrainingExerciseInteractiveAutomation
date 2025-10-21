namespace Automation_1.ParameterValueSelection
{
    using System;
    using System.Linq;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Net.Messages;

    public class ParamtereValueSelectionPresenter
    {
        private readonly IElementSelector _model;
        private readonly IParameterValueSelectionView _view;
        private IEngine _engine;

        public ParamtereValueSelectionPresenter(IElementSelector model, IParameterValueSelectionView view, IEngine engine)
        {
            _model=model ?? throw new ArgumentNullException(nameof(view));
            _view=view ?? throw new ArgumentNullException(nameof(model));
            _engine=engine ?? throw new ArgumentNullException(nameof(engine));

            view.ExitButton.Pressed += OnExitButtonPressed;
            view.BackButton.Pressed += OnBackButtonPressed;
            view.SetStringVauleButton.Pressed += OnSetStringValueButtonPressed;
            view.SetDoubleValueButton.Pressed += OnSetDoubleValueButtonPressed;
        }

        public event EventHandler<EventArgs> Back;

        public event EventHandler<EventArgs> Exit;

        private void OnBackButtonPressed(object sender, EventArgs e)
        {
            Back?.Invoke(this, EventArgs.Empty);
        }

        private void OnExitButtonPressed(object sender, EventArgs e)
        {
            Exit?.Invoke(this, EventArgs.Empty);
        }

        public void OnSetStringValueButtonPressed(object sender, EventArgs e)
        {
            try
            {
                string valueToSet = _view.StringValueTextBox.Text;

                if (String.IsNullOrWhiteSpace(valueToSet))
                {
                    _view.ExceptionTextBox.Text = "Please enter a valid string value";
                    return;
                }

                _model.StringValue = valueToSet;

                var element = _model.SelectedElement;

                bool elementIsActive = _engine.FindElement(element.AgentId, element.Id).IsActive;

                if (!elementIsActive)
                {
                    _view.ExceptionTextBox.Text = "The element is inactive";
                    return;
                }

                var parameterId = _model.SelectedParameter;

                var type = _model.Parameters.Where(parameters => parameters.ID == parameterId).First().InterpreteType;

                if (type != ParameterInterpreteType.String)
                {
                    _view.ExceptionTextBox.Text = "Parameter isn't of type strng";
                    return;
                }

                var parameter = element.GetStandaloneParameter<string>(parameterId);

                parameter.SetValue(valueToSet);
                _view.ExceptionTextBox.Text = "Success";
            }
            catch (Exception ex)
            {
                _engine.Log($"Exception: {ex}");
                _view.ExceptionTextBox.Text = $"Error: {ex.Message}";
            }
        }

        public void OnSetDoubleValueButtonPressed(object sender, EventArgs e)
        {
            try
            {
                double valueToSet = _view.DoubleValueNumeric.Value;

                _model.DoubleValue = valueToSet;

                var element = _model.SelectedElement;

                bool elementIsActive = _engine.FindElement(element.AgentId, element.Id).IsActive;

                if (!elementIsActive)
                {
                    _view.ExceptionTextBox.Text = "The element is inactive";
                    return;
                }

                var parameterId = _model.SelectedParameter;

                var parameterData = _model.Parameters.Where(parameters => parameters.ID == parameterId).First();

                var type = parameterData.InterpreteType;

                if (type != ParameterInterpreteType.Double)
                {
                    _view.ExceptionTextBox.Text = "Parameter isn't of type double";
                    return;
                }

                var maxRange = parameterData.RangeHigh;
                var minRange = parameterData.RangeLow;

                if (valueToSet < minRange || valueToSet > maxRange)
                {
                    _view.ExceptionTextBox.Text = "Double value is outside of parameter range";
                    return;
                }

                var parameter = element.GetStandaloneParameter<double?>(parameterId);

                parameter.SetValue(valueToSet);
                _view.ExceptionTextBox.Text ="Success";
            }
            catch (Exception ex)
            {
                _engine.Log($"Exception: {ex}");
                _view.ExceptionTextBox.Text = $"Error: {ex.Message}";
            }
        }
    }
}
