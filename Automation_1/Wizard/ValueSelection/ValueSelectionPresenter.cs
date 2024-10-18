namespace Automation_1.Wizard.ValueSelection
{
	using System;

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

		private void StoreToModel()
		{
			string value = _view.Input.Text;
			_model.NewParameterValue = value;
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
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
