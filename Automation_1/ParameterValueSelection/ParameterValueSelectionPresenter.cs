namespace Automation_1.ParameterValueSelection
{
	using System;
	using System.Linq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Messages;

	public class ParameterValueSelectionPresenter
	{
		private readonly IParameterValueSelectionView view;

		private readonly IElementSelector selector;

		private readonly IEngine engine;

		public ParameterValueSelectionPresenter(IEngine engine, IParameterValueSelectionView parameterView, IElementSelector elementSelector)
		{
			view = parameterView ?? throw new ArgumentNullException(nameof(parameterView));
			selector = elementSelector ?? throw new ArgumentNullException(nameof(elementSelector));
			this.engine = engine;
			view.SetStringValue.Pressed += OnSetStringValuePressed;
			view.SetDoubleValue.Pressed += OnSetDoubleValuePressed;
			view.BackButton.Pressed += OnBackButtonPressed;
			view.ExitButton.Pressed += OnExitButtonPressed;
		}

		public event EventHandler<EventArgs> SetStringValue;

		public event EventHandler<EventArgs> SetDoubleValue;

		public event EventHandler<EventArgs> Back;

		public event EventHandler<EventArgs> Exit;

		public void OnSetStringValue()
		{
			try
			{
				string valueToSet = view.StringValue.Text;

				if (String.IsNullOrWhiteSpace(valueToSet))
				{
					view.Message.Text = "String value cannot be empty.";
					return;
				}

				selector.SetParameterValueString = valueToSet;
				var element = selector.SelectedElement;
				var parameterId = selector.SelectedParameterId;

				var type = selector.Parameters.Where(parameterInfo => parameterInfo.ID == parameterId).First().InterpreteType;
				bool checkedElementState = engine.FindElement(element.AgentId, element.Id).IsActive;

				if (!checkedElementState)
				{
					view.Message.Text = "Selected element is inactive!";
					return;
				}

				if (type != ParameterInterpreteType.String)
				{
					view.Message.Text = "Parameter is not type of string";
					return;
				}

				if (parameterId <= 0)
				{
					view.Message.Text = "Invalid parameter ID.";
					return;
				}

				var parameter = element.GetStandaloneParameter<string>(parameterId);
				parameter.SetValue(valueToSet);
				view.Message.Text = "String parameter set successfull!";
			}
			catch (Exception ex)
			{
				view.Message.Text = $"Failed to set string parameter: {ex.Message}";
			}
		}

		public void OnSetDoubleValue()
		{
			try
			{
				double valueToSet = view.DoubleValue.Value;

				selector.SetParameterValueDouble = valueToSet;
				var element = selector.SelectedElement;
				var parameterId = selector.SelectedParameterId;
				var parameter = selector.Parameters.Where(parameterInfo => parameterInfo.ID == parameterId).FirstOrDefault();

				if (parameter == null)
				{
					view.Message.Text = "There are no parameters for selected parameter id!";
					return;
				}

				var type = parameter.InterpreteType;
				bool hasRange = parameter.HasRange;
				bool checkedElementState = engine.FindElement(element.AgentId, element.Id).IsActive;

				if (!checkedElementState)
				{
					view.Message.Text = "Selected element is inactive!";
					return;
				}

				if (type != ParameterInterpreteType.Double)
				{
					view.Message.Text = "Parameter is not type of double!";
					return;
				}

				if (hasRange)
				{
					var rangeMax = parameter.RangeHigh;
					var rangeMin = parameter.RangeLow;

					if(valueToSet<rangeMin || valueToSet > rangeMax)
					{
						view.Message.Text = "Parameter value is out of range of the parameter!";
						return;
					}
				}

				if (parameterId <= 0)
				{
					view.Message.Text = "Invalid parameter ID.";
					return;
				}

				var elementParameter = element.GetStandaloneParameter<double?>(parameterId);
				elementParameter.SetValue(valueToSet);
				view.Message.Text = "Double parameter set successfull!";
			}
			catch (Exception ex)
			{
				view.Message.Text = $"Failed to set double parameter: {ex.Message}";
			}
		}

		private void OnSetDoubleValuePressed(object sender, EventArgs e)
		{
			OnSetDoubleValue();

			SetDoubleValue?.Invoke(this, EventArgs.Empty);
		}

		private void OnSetStringValuePressed(object sender, EventArgs e)
		{
			OnSetStringValue();

			SetStringValue?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			Back?.Invoke(this, EventArgs.Empty);
		}

		private void OnExitButtonPressed(object sender, EventArgs e)
		{
			Exit?.Invoke(this, EventArgs.Empty);
		}
	}
}
