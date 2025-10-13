namespace InteractiveAutomation.Wizard.ElementSelection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Core.DataMinerSystem.Common.Selectors;

	internal class ElementSelectionPresenter
	{
		private readonly IElementSelectionView elementSelectionView;
		private readonly IModel model;
		private Dictionary<string, IDmsElement> elementsDictionary;

		public ElementSelectionPresenter(IElementSelectionView view, IModel model)
		{
			this.elementSelectionView = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));

			elementSelectionView.NextButton.Pressed += OnNextPressed;
		}

		public event EventHandler<EventArgs> Next;

		public void LoadFromModel()
		{
			// Load elements from model
			elementsDictionary = model.Elements.ToDictionary(element => element.Name);

			// Set elements to DropDown
			elementSelectionView.ElementsDropDown.SetOptions(elementsDictionary.Keys);

			// Set default value TODO: is this needed?
			elementSelectionView.ElementsDropDown.Selected = "main-ird";
		}

		private void OnNextPressed(object sender, EventArgs e)
		{
			// TODO moeten we checken of er iets geselecteerd is?
			StoreToModel();
			Next?.Invoke(this, EventArgs.Empty);

			/*if (selectedElements.Any()) // TODO deze check eens bekijken (ik denk dat je beter controleert of model.Selected != null
			{
				StoreToModel();
				Next?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				ShowValidationProblem("At least one element must be selected.");
			}*/
		}

		private void ShowValidationProblem(string s)
		{
			Next?.Invoke(this, EventArgs.Empty);

			// elementSelectionView.ValidationLabel.Text = s;
			// elementSelectionView.ValidationLabel.IsVisible = true;
		}

		private void StoreToModel()
		{
			model.SelectedElement = elementsDictionary[elementSelectionView.ElementsDropDown.Selected];
			
			// string selected = elementSelectionView.ElementsDropDown.Selected;

			// Write selected to model

			// Needed?
			// model.SelectedElements.Clear();
			// foreach (IDmsElement selectedElement in selectedElements)
			// {
			// 	model.SelectedElements.Add(selectedElement);
			// }
		}
	}
}
