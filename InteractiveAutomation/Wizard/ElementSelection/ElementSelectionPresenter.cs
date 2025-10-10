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
		private readonly HashSet<IDmsElement> selectedElements = new HashSet<IDmsElement>();

		public ElementSelectionPresenter(IElementSelectionView view)
		{
			elementSelectionView = view ?? throw new ArgumentNullException(nameof(view));

			elementSelectionView.NextButton.Pressed += OnNextPressed;
		}

		public event EventHandler<EventArgs> Next;

		public void LoadFromModel()
		{
			// Load protocols from model

			// Set protocols retrieved via model from dms to DropDown
			// elementSelectionView.ElementsDropDown.SetOptions()
			elementSelectionView.ElementsDropDown.Selected = $"This is still hardcoded but should be retrieved from model";
		}

		private void OnNextPressed(object sender, EventArgs e)
		{
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
