namespace Automation_1.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ElementSelectionPresenter
	{
		private readonly IElementSelector selector;

		private readonly ElementSelectionView view;

		private Dictionary<string, IDmsElement> elementsByName;

		public ElementSelectionPresenter(ElementSelectionView elementView, IElementSelector elementSelector)
		{
			selector = elementSelector;
			view = elementView;

			view.ContinueButton.Pressed += OnContinueButtonPressed;
		}

		public event EventHandler<EventArgs> Continue;

		public void LoadFromModel()
		{
			elementsByName = selector.Elements.ToDictionary(element => element.Name);

			view.ElementsDropDown.SetOptions(elementsByName.Keys);
			view.ElementsDropDown.Selected = selector.SelectedElement.Name;
		}

		private void StoreToModel()
		{
			string selected = view.ElementsDropDown.Selected;
			selector.SelectedElement = elementsByName[selected];
		}

		private void OnContinueButtonPressed(object sender, EventArgs e)
		{
			StoreToModel();

			Continue?.Invoke(this, EventArgs.Empty);
		}
	}
}
