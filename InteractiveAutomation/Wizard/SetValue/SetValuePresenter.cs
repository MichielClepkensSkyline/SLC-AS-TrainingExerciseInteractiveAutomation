namespace InteractiveAutomation.Wizard.SetValue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using InteractiveAutomation.Wizard.ParameterSelection;

	internal class SetValuePresenter
	{
		private readonly ISetValueView setValueView;

		public SetValuePresenter(ISetValueView view)
		{
			setValueView = view ?? throw new ArgumentNullException(nameof(view));

			setValueView.StringButton.Pressed += OnSetStringPressed;
			setValueView.FinishButton.Pressed += OnFinishPressed;
			setValueView.BackButton.Pressed += OnBackPressed;
		}

		public event Action<String> SetString;

		public event EventHandler<EventArgs> Finish;

		public event EventHandler<EventArgs> Back;

		private void OnSetStringPressed(object sender, EventArgs e)
		{
			string value = setValueView.StringValueBox.Text;
			SetString?.Invoke(value);
		}

		private void OnFinishPressed(object sender, EventArgs e)
		{
			// StoreToModel
			Finish?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackPressed(object sender, EventArgs e)
		{
			// StoreToModel
			Back?.Invoke(this, EventArgs.Empty);
		}
	}
}
