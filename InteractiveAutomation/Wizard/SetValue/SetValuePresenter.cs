using InteractiveAutomation.Wizard.ParameterSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAutomation.Wizard.SetValue
{
	internal class SetValuePresenter
	{
		private readonly ISetValueView setValueView;

		public SetValuePresenter(ISetValueView view)
		{
			setValueView = view ?? throw new ArgumentNullException(nameof(view));

			setValueView.FinishButton.Pressed += OnFinishPressed;
		}

		public event EventHandler<EventArgs> Finish;

		private void OnFinishPressed(object sender, EventArgs e)
		{
			Finish?.Invoke(this, EventArgs.Empty);
		}
	}
}
