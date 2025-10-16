using Skyline.DataMiner.Core.DataMinerSystem.Common;
using Skyline.DataMiner.Net.Helper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection
{
	public class ParameterValueSelectionPresenter
	{
		private readonly IParameterValueSelectionView view;

		private readonly IElementSelector selector;

		public ParameterValueSelectionPresenter(IParameterValueSelectionView parameterView, IElementSelector elementSelector)
		{
			view = parameterView ?? throw new ArgumentNullException(nameof(parameterView));
			selector = elementSelector ?? throw new ArgumentNullException(nameof(elementSelector));

			view.SetStringValue.Pressed += OnSetStringValuePressed;
			view.SetDoubleValue.Pressed += OnSetDoubleValuePressed;
		}

		private void OnSetStringValuePressed(object sender, EventArgs e)
		{
			string valueToSet = view.StringValue.Text;

			if (string.IsNullOrWhiteSpace(valueToSet))
			{
				view.Message.Text = "String value cannot be empty.";
				return;
			}

			var element = selector.SelectedElement;
			var parameterId = selector.SelectedParameterId;

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

			try
			{
				var parameter = element.GetStandaloneParameter<string>(parameterId);
				parameter.SetValue(valueToSet);

				selector.SetParameterValueString = valueToSet;
				view.Message.Text = "String parameter set successfull!";
			}
			catch (Exception ex)
			{
				view.Message.Text = $"Failed to set string parameter: {ex.Message}";
			}
		}

		private void OnSetDoubleValuePressed(object sender, EventArgs e)
		{
			double valueToSet = view.DoubleValue.Value;

			var element = selector.SelectedElement;
			var parameterId = selector.SelectedParameterId;

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

			try
			{
				var parameter = element.GetStandaloneParameter<double?>(parameterId);
				parameter.SetValue(valueToSet);
				view.Message.Text = "Double parameter set successfully.";
			}
			catch (Exception ex)
			{
				view.Message.Text = $"Failed to set double parameter: {ex.Message}";
			}
		}

		private bool IsElementValid(IDmsElement element)
		{
			return element != null && element.State == ElementState.Active;
		}
	}
}
