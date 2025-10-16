namespace Automation_1.ParameterSelection
{
	using Automation_1.ElementSelection;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Core.DataMinerSystem.Common.Selectors;
	using Skyline.DataMiner.Net.Messages;

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
		private Dictionary<string, ParameterInfo> parametersByName;

		public ParameterSelectionPresenter(IEngine engine, IParameterSelectionView view, IElementSelector element)
		{
			this.engine = engine;
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(parameterSelectionView));
			elementSelector = element ?? throw new ArgumentNullException(nameof(elementSelector));

			//this.elementSelector.SelectedParameterChanged += OnSelectedParameterChanged;
			parameterSelectionView.ContinueButton.Pressed += OnContinueButtonPressed;

			parameterSelectionView.BackButton.Pressed += OnBackButtonPressed;

			/*parameterSelectionView.ParameterId.Changed += OnParameterIdChanged;

			UpdateContinueButtonState();*/
		}

		public event EventHandler<EventArgs> Continue;

		public event EventHandler<EventArgs> Back;

		private void OnParameterIdChanged(object sender, EventArgs e)
		{
			StoreToModel();
		}

		public void LoadFromModel()
		{
			if (elementSelector.Elements == null || !elementSelector.Elements.Any())
			{
				parameterSelectionView.ParameterId.SetOptions(new List<string>());
				return;
			}

			if (elementSelector.Parameters == null || !elementSelector.Parameters.Any())
			{
				engine.Log("No parameters found for the selected element.");
				parameterSelectionView.ParameterId.SetOptions(new List<string>());
				return;
			}

			parametersByName = elementSelector.Parameters.ToDictionary(p => p.ID.ToString());

			parameterSelectionView.ParameterId.SetOptions(parametersByName.Keys);
			parameterSelectionView.ParameterId.Selected = elementSelector.SelectedParameterId.ToString();
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
			string selected = parameterSelectionView.ParameterId.Selected;
			elementSelector.SelectedParameterId = parametersByName[selected].ID;
			//elementSelector.SelectedParameterId = (int)parameterSelectionView.ParameterId.Value;
		}
	}
}
