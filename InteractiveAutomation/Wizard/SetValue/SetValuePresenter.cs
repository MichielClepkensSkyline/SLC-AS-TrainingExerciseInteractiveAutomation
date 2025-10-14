namespace InteractiveAutomation.Wizard.SetValue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using InteractiveAutomation.Wizard.ParameterSelection;
	using Skyline.DataMiner.Core.DataMinerSystem.Common.Selectors;

	internal class SetValuePresenter
	{
		private readonly ISetValueView setValueView;

		public SetValuePresenter(ISetValueView view)
		{
			setValueView = view ?? throw new ArgumentNullException(nameof(view));

			setValueView.StringButton.Pressed += OnSetStringPressed;
			setValueView.DoubleButton.Pressed += OnSetDoublePressed;
			setValueView.FinishButton.Pressed += OnFinishPressed;
			setValueView.BackButton.Pressed += OnBackPressed;
		}

		public event Action<String> SetString;

		public event Action<Double> SetDouble;

		public event EventHandler<EventArgs> Finish;

		public event EventHandler<EventArgs> Back;

		public void DisplayMessage(string message)
		{
			setValueView.MessageBox.Text = message;
		}

		private void OnSetStringPressed(object sender, EventArgs e)
		{
			string value = setValueView.StringValueBox.Text;
			SetString?.Invoke(value);
		}

		private void OnSetDoublePressed(object sender, EventArgs e)
		{
			double value = setValueView.DoubleValueBox.Value;
			SetDouble?.Invoke(value);
		}

		private void OnFinishPressed(object sender, EventArgs e)
		{
			Finish?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackPressed(object sender, EventArgs e)
		{
			// StoreToModel TODO implementeren dat waarde die eerder zet is onthouden wordt?
			Back?.Invoke(this, EventArgs.Empty);
		}
	}
}
