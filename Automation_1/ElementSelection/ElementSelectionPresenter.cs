namespace Automation_1.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ElementSelectionPresenter
	{
		private readonly IElementSelector selector;

		private readonly IElementSelectionView view;

		private Dictionary<string, IDmsElement> elementsByName;

		public ElementSelectionPresenter(IElementSelectionView elementView, IElementSelector elementSelector)
		{
			selector = elementSelector ?? throw new ArgumentNullException(nameof(elementSelector));
			view = elementView ?? throw new ArgumentNullException(nameof(elementView));

			view.ContinueButton.Pressed += OnContinueButtonPressed;
		}

		public event EventHandler<EventArgs> Continue;

		public void LoadFromModel()
		{
			if (selector.Elements == null || !selector.Elements.Any())
			{
				view.ElementsDropDown.SetOptions(new List<string>());
				return;
			}

			elementsByName = selector.Elements.Where(element => element != null && !string.IsNullOrWhiteSpace(element.Name)).ToDictionary(element => element.Name);

			view.ElementsDropDown.SetOptions(elementsByName.Keys);
			view.ElementsDropDown.Selected = selector.SelectedElement.Name;
		}

		private void StoreToModel()
		{
			string selectedElement = view.ElementsDropDown.Selected;
			selector.SelectedElement = elementsByName[selectedElement];
		}

		private void OnContinueButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();

			Continue?.Invoke(this, EventArgs.Empty);
		}
	}
}
