namespace Automation_1.Wizard.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ElementSelectionPresenter
	{
		private readonly IElementSelectionView _view;
		private readonly IParameterSetter _model;

		private Dictionary<string, IDmsElement> _elementsByName;

		public ElementSelectionPresenter(IElementSelectionView view, IParameterSetter model)
		{
			_view = view ?? throw new ArgumentNullException(nameof(view));
			_model = model ?? throw new ArgumentNullException(nameof(model));

			_view.ContinueButton.Pressed += OnContinueButtonPressed;
		}

		public event EventHandler<EventArgs> Continue;

		public void LoadFromModel()
		{
			_elementsByName = _model.Elements.ToDictionary(element => $"{element.Name}");

			_view.ElementsDropDown.SetOptions(_elementsByName.Keys);
			_view.ElementsDropDown.Selected = _model.SelectedElement?.Name ?? string.Empty;
		}

		private void StoreToModel()
		{
			string selected = _view.ElementsDropDown.Selected;
			_model.SelectedElement = _elementsByName[selected];
		}

		private void OnContinueButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Continue?.Invoke(this, EventArgs.Empty);
		}
	}
}
