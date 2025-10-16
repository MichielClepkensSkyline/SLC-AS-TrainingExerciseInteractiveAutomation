namespace InteractiveAutomation.Wizard.SetValue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using InteractiveAutomation.Wizard.ParameterSelection;
	using Skyline.DataMiner.Core.DataMinerSystem.Common.Selectors;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class SetValuePresenter
	{
		private readonly SetDoubleView setDoubleView;
		private readonly SetStringView setStringView;
		private readonly SetEmptyView setEmptyView;
		private readonly IModel model;

		public SetValuePresenter(SetDoubleView setDoubleView, SetStringView setStringView, SetEmptyView setEmptyView, IModel model)
		{
			this.setDoubleView = setDoubleView ?? throw new ArgumentNullException(nameof(setDoubleView));
			this.setStringView = setStringView ?? throw new ArgumentNullException(nameof(setStringView));
			this.setEmptyView = setEmptyView ?? throw new ArgumentNullException(nameof(setEmptyView));
			this.model = model ?? throw new ArgumentNullException(nameof(model));

			this.setDoubleView.SetButton.Pressed += OnSetDoublePressed;
			this.setStringView.SetButton.Pressed += OnSetStringPressed;
			this.setEmptyView.SetButton.Pressed += OnSetEmptyPressed;

			this.setDoubleView.BackButton.Pressed += OnBackPressed;
			this.setStringView.BackButton.Pressed += OnBackPressed;
			this.setEmptyView.BackButton.Pressed += OnBackPressed;

			this.setDoubleView.FinishButton.Pressed += OnFinishPressed;
			this.setStringView.FinishButton.Pressed += OnFinishPressed;
			this.setEmptyView.FinishButton.Pressed += OnFinishPressed;
		}

		public event EventHandler<EventArgs> Finish;

		public event EventHandler<EventArgs> Back;

		private void OnFinishPressed(object sender, EventArgs e)
		{
			Finish?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackPressed(object sender, EventArgs e)
		{
			Back?.Invoke(this, EventArgs.Empty);
		}

		private void DisplayMessage(ISetValueView view, string message)
		{
			view.MessageBox.Text = message;
		}

		private void OnSetDoublePressed(object sender, EventArgs e)
		{
			double value = setDoubleView.ValueBox.Value;
			string message = model.SetDoubleOnParameter(value);
			DisplayMessage(setDoubleView, message);
		}

		private void OnSetStringPressed(object sender, EventArgs e)
		{
			string value = setStringView.ValueBox.Text;
			string message = model.SetStringOnParameter(value);
			DisplayMessage(setStringView, message);
		}

		private void OnSetEmptyPressed(object sender, EventArgs e)
		{
			string message = "Not implemented yet!";
			DisplayMessage(setDoubleView, message);
		}
	}
}
