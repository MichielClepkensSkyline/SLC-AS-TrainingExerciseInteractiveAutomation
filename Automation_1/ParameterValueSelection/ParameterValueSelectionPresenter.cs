using Automation_1.ParameterSelection;

using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Core.DataMinerSystem.Common;
using Skyline.DataMiner.Net.Helper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection
{
	public class ParameterValueSelectionPresenter
	{
		private readonly IParameterValueSelectionView view;

		private readonly IElementSelector selector;
		private readonly IEngine engine;

		public ParameterValueSelectionPresenter(IEngine engine, IParameterValueSelectionView parameterView, IElementSelector elementSelector)
		{
			view = parameterView ?? throw new ArgumentNullException(nameof(parameterView));
			selector = elementSelector ?? throw new ArgumentNullException(nameof(elementSelector));

			view.SetStringValue.Pressed += OnSetStringValuePressed;
			view.SetDoubleValue.Pressed += OnSetDoubleValuePressed;
			view.BackButton.Pressed += OnBackButtonPressed;
		}

		public event EventHandler<EventArgs> SetStringValue;

		public event EventHandler<EventArgs> SetDoubleValue;

		public event EventHandler<EventArgs> Back;

		private void OnSetStringValue()
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

				var type = selector.Parameters.Where(p => p.ID == parameterId).First().InterpreteType.ToString();

				if(!type.Contains("String"))
				{
					view.Message.Text = "Parameter is not type of string";
					return;
				}

				if (!IsElementValid(element))
				{
					view.Message.Text = "Selected element is invalid or inactive.";
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

		private void OnSetDoubleValue()
		{
			try
			{
				double valueToSet = view.DoubleValue.Value;

				var element = selector.SelectedElement;
				var parameterId = selector.SelectedParameterId;

				var type = selector.Parameters.Where(p => p.ID == parameterId).First().InterpreteType.ToString();

				if (!type.Contains("Double"))
				{
					view.Message.Text = "Parameter is not type of double";
					return;
				}

				if (!IsElementValid(element))
				{
					view.Message.Text = "Selected element is invalid or inactive.";
					return;
				}

				if (parameterId <= 0)
				{
					view.Message.Text = "Invalid parameter ID.";
					return;
				}

				var parameter = element.GetStandaloneParameter<double?>(parameterId);
				parameter.SetValue(valueToSet);
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

		private bool IsElementValid(IDmsElement element)
		{
			return element != null && element.State == ElementState.Active;
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			Back?.Invoke(this, EventArgs.Empty);
		}
	}
}
