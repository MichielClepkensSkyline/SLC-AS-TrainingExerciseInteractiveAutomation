namespace Automation_1.ParameterSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Messages;

	public class ParameterSelectionPresenter
	{
		private readonly IParameterSelectionView parameterSelectionView;
		private readonly IElementSelector elementSelector;
		private readonly IEngine engine;
		private Dictionary<string, ParameterInfo> parametersByName;

		public ParameterSelectionPresenter(IEngine engine, IParameterSelectionView view, IElementSelector element)
		{
			this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(view));
			elementSelector = element ?? throw new ArgumentNullException(nameof(element));

			parameterSelectionView.ContinueButton.Pressed += OnContinueButtonPressed;

			parameterSelectionView.BackButton.Pressed += OnBackButtonPressed;
		}

		public event EventHandler<EventArgs> Continue;

		public event EventHandler<EventArgs> Back;

		public void LoadFromModel()
		{
			if (elementSelector.Elements == null || !elementSelector.Elements.Any())
			{
				engine.Log("No elements found for the selected dms.");
				parameterSelectionView.ParameterIdDropDown.SetOptions(new List<string>());
				return;
			}

			if (elementSelector.Parameters == null || !elementSelector.Parameters.Any())
			{
				engine.Log("No parameters found for the selected element.");
				parameterSelectionView.ParameterIdDropDown.SetOptions(new List<string>());
				return;
			}

			parametersByName = elementSelector.Parameters.ToDictionary(parameter => $"{parameter.Name} ({parameter.ID.ToString()})");

			parameterSelectionView.ParameterIdDropDown.SetOptions(parametersByName.Keys);
			parameterSelectionView.ParameterIdDropDown.Selected = elementSelector.SelectedParameterId.ToString();
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
			string selectedParameter = parameterSelectionView.ParameterIdDropDown.Selected;
			elementSelector.SelectedParameterId = parametersByName[selectedParameter].ID;
		}
	}
}
