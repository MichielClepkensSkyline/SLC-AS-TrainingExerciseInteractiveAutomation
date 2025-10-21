namespace Automation_1.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ElementSelectionPresenter
	{
		private readonly IElementSelector selector;

		private readonly IElementSelectionView view;

		private readonly IEngine engine;

		private Dictionary<string, IDmsElement> elementsByName;

		public ElementSelectionPresenter(IEngine engine, IElementSelectionView elementView, IElementSelector elementSelector)
		{
			selector = elementSelector ?? throw new ArgumentNullException(nameof(elementSelector));
			view = elementView ?? throw new ArgumentNullException(nameof(elementView));
			this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
			view.ContinueButton.Pressed += OnContinueButtonPressed;
		}

		public event EventHandler<EventArgs> Continue;

		public void LoadFromModel()
		{
			if (selector.Elements == null || !selector.Elements.Any())
			{
				engine.Log("No elements found for the selected dms.");
				view.ElementsDropDown.SetOptions(new List<string>());
				return;
			}

			elementsByName = selector.Elements.ToDictionary(element => element.Name);

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
