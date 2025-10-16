namespace Automation_1.ParameterSelection
{
	using Automation_1.ElementSelection;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common.Selectors;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ParameterSelectionPresenter
	{
		private readonly IParameterSelectionView parameterSelectionView;
		private readonly IElementSelector elementSelector;
		private readonly IEngine engine;

		public ParameterSelectionPresenter(IEngine engine, IParameterSelectionView view, IElementSelector element)
		{
			this.engine = engine;
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(parameterSelectionView));
			elementSelector = element ?? throw new ArgumentNullException(nameof(elementSelector));

			this.elementSelector.SelectedParameterChanged += OnSelectedParameterChanged;
			parameterSelectionView.ContinueButton.Pressed += OnContinueButtonPressed;

			parameterSelectionView.BackButton.Pressed += OnBackButtonPressed;

			parameterSelectionView.ParameterId.Changed += OnParameterIdChanged;

			UpdateContinueButtonState();
		}

		public event EventHandler<EventArgs> Continue;

		public event EventHandler<EventArgs> Back;

		private void OnParameterIdChanged(object sender, EventArgs e)
		{
			StoreToModel();
		}

		private void OnSelectedParameterChanged(object sender, EventArgs e)
		{
			//engine.Log($"Parameter changed. Valid: {elementSelector.IsParameterValid}");
			UpdateContinueButtonState();
		}

		private void UpdateContinueButtonState()
		{
			parameterSelectionView.ContinueButton.IsEnabled = elementSelector.IsParameterValid;
		}

		private void OnContinueButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();

			Continue?.Invoke(this, EventArgs.Empty);
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();

			Back?.Invoke(this, EventArgs.Empty);
		}

		private void StoreToModel()
		{
			elementSelector.SelectedParameterId = (int)parameterSelectionView.ParameterId.Value;
		}
	}
}
