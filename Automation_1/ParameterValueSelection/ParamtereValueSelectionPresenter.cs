using Skyline.DataMiner.Automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection
{
    public class ParamtereValueSelectionPresenter
    {
        using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Core.DataMinerSystem.Common.Selectors;
using Skyline.DataMiner.Net.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelector
    {
        public class ParameterValueSelectorPresenter
        {
            private readonly IElementSelector _model;
            private readonly IParameterValueSelectionView _view;
            private IEngine _engine;

            public ParameterValueSelectorPresenter(IElementSelector model, IParameterValueSelectionView view, IEngine engine)
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

            private void OnSetStringValueButtonPressed(object sender, EventArgs e)
            {
                try
                {
                    string valueToSet = _view.StringValueTextBox.Text;

                    if (String.IsNullOrWhiteSpace(valueToSet))
                    {
                        _view.ExceptionTextBox.Text = "Please enter a valid string value";
                    }
                    else
                    {
                        _model.StringValue = valueToSet;

                        var element = _model.SelectedElement;

                        var parameterId = _model.SelectedParameter;

                        var parameter = element.GetStandaloneParameter<string>(parameterId);
                        parameter.SetValue(valueToSet);

                        _view.ExceptionTextBox.Text = "Success";
                    }

                }
                catch (Exception ex)
                {
                    _engine.Log($"Exception: {ex}");
                    _view.ExceptionTextBox.Text = $"Error: {ex.Message}";
                }
            }

            private void OnSetDoubleValueButtonPressed(object sender, EventArgs e)
            {
                try
                {
                    double valueToSet = _view.DoubleValueNumeric.Value;

                    if (valueToSet<0)
                    {
                        _view.ExceptionTextBox.Text = "Please enter a valid double value";
                    }
                    else
                    {
                        _model.DoubleValue = valueToSet;

                        var element = _model.SelectedElement;

                        var parameterId = _model.SelectedParameter;

                        var parameter = element.GetStandaloneParameter<double?>(parameterId);
                        parameter.SetValue(valueToSet);

                        _view.ExceptionTextBox.Text ="Success";
                    }
                }
                catch (Exception ex)
                {
                    _engine.Log($"Exception: {ex}");
                    _view.ExceptionTextBox.Text = $"Error: {ex.Message}";
                }
            }
        }
    }

}
}
