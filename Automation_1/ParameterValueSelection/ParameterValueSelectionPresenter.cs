using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection
{
	public class ParameterValueSelectionPresenter
	{
		private readonly ParameterValueSelectionView view;

		private readonly IElementSelector selector;

		public ParameterValueSelectionPresenter(ParameterValueSelectionView parameterView, IElementSelector elementSelector)
		{
			view = parameterView ?? throw new ArgumentNullException(nameof(parameterView));
			selector = elementSelector ?? throw new ArgumentNullException(nameof(elementSelector));

			view.SetStringValue.Pressed += OnSetStringValuePressed;
		}

		private void OnSetStringValuePressed(object sender, EventArgs e)
		{
			string valueToSet = view.StringValue.Text;

			selector.SetParameterValueString = valueToSet;

			var element = selector.SelectedElement;
			var parameterId = selector.SelectedParameterId;

			if (element == null)
			{
				throw new InvalidOperationException("No element is selected.");
			}

			if (parameterId <= 0)
			{
				throw new InvalidOperationException("Invalid parameter ID selected.");
			}

			var parameter = element.GetStandaloneParameter<string>(parameterId);
			parameter.SetValue(valueToSet);
		}
	}
}
