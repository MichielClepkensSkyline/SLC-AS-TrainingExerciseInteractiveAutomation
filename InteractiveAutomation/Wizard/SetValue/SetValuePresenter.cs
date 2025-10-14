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
		private readonly IModel model;

		public SetValuePresenter(ISetValueView view, IModel model)
		{
			setValueView = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));

			setValueView.StringButton.Pressed += OnSetStringPressed;
			setValueView.DoubleButton.Pressed += OnSetDoublePressed;
			setValueView.FinishButton.Pressed += OnFinishPressed;
			setValueView.BackButton.Pressed += OnBackPressed;
		}

		public event EventHandler<EventArgs> Finish;

		public event EventHandler<EventArgs> Back;

		private void OnFinishPressed(object sender, EventArgs e)
		{
			Finish?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackPressed(object sender, EventArgs e)
		{
			// StoreToModel TODO implementeren dat waarde die eerder zet is onthouden wordt?
			Back?.Invoke(this, EventArgs.Empty);
		}

		private void DisplayMessage(string message)
		{
			setValueView.MessageBox.Text = message;
		}

		private void OnSetStringPressed(object sender, EventArgs e)
		{
			string value = setValueView.StringValueBox.Text;
			string message = model.SetStringOnParameter(value);
			DisplayMessage(message);
		}

		private void OnSetDoublePressed(object sender, EventArgs e)
		{
			double value = setValueView.DoubleValueBox.Value;
			string message = model.SetDoubleOnParameter(value);
			DisplayMessage(message);
		}
	}
}
