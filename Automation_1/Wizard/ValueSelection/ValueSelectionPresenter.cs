namespace Automation_1.Wizard.ValueSelection
{
    using System;
    using Automation_1.Model;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public class ValueSelectionPresenter
	{
		private readonly IValueSelectionView _view;
		private readonly IParameterSetter _model;

		public ValueSelectionPresenter(IValueSelectionView view, IParameterSetter model)
		{
			_view = view ?? throw new ArgumentNullException(nameof(view));
			_model = model ?? throw new ArgumentNullException(nameof(model));

			_view.BackButton.Pressed += OnBackButtonPressed;
			_view.ExitButton.Pressed += OnExitButtonPressed;
			_view.FinishButton.Pressed += OnFinishButtonPressed;
		}

		public event EventHandler<EventArgs> Back;

		public event EventHandler<EventArgs> Exit;

		public event EventHandler<EventArgs> Finish;

		public void LoadFromModel()
		{
			string value = _model.NewParameterValue;

			if (_view.CurrentInput is TextBox textBox)
			{
				textBox.Text = value;
			}
			else if (_view.CurrentInput is DateTimePicker dateTimePicker)
			{
				if (DateTime.TryParseExact(
					value,
					"MM/dd/yyyy hh:mm:ss tt",
					System.Globalization.CultureInfo.InvariantCulture,
					System.Globalization.DateTimeStyles.None,
					out DateTime dateTime))
				{
					dateTimePicker.DateTime = dateTime;
				}
				else
				{
					dateTimePicker.DateTime = DateTime.Now;
				}
			}
			else if (_view.CurrentInput is Numeric numeric)
			{
				if (double.TryParse(value, out double numericValue))
				{
					numeric.Value = numericValue;
				}
				else
				{
					numeric.Value = 0;
				}
			}
			else
			{
				throw new InvalidOperationException($"Unsupported widget type: {_view.CurrentInput.GetType().Name}");
			}
		}

		private void StoreToModel()
		{
			string value = string.Empty;

			if (_view.CurrentInput is TextBox textBox)
			{
				value = textBox.Text;
			}
			else if (_view.CurrentInput is DateTimePicker dateTimePicker)
			{
				value = dateTimePicker.DateTime.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			else if (_view.CurrentInput is Numeric numeric)
			{
				value = numeric.Value.ToString();
			}
			else
			{
				throw new InvalidOperationException($"Unsupported widget type: {_view.CurrentInput.GetType().Name}");
			}

			_model.NewParameterValue = value;
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
			_view.SetFeedbackMessage(string.Empty);
			Back?.Invoke(this, EventArgs.Empty);
		}

		private void OnExitButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Exit?.Invoke(this, EventArgs.Empty);
		}

		private void OnFinishButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Finish?.Invoke(this, EventArgs.Empty);
		}
	}
}
