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
		private readonly IEngine engine;

		public ElementSelectionPresenter(IElementSelectionView view, IModel model, IEngine engine)
		{
			this.elementSelectionView = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));
			this.engine = engine;

			elementSelectionView.NextButton.Pressed += OnNextPressed;
		}

		public event EventHandler<EventArgs> Next;

		public void LoadFromModel()
		{
			// Set elements to DropDown
			elementSelectionView.ElementsDropDown.SetOptions(model.Elements.Keys);

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

		private void StoreToModel()
		{
			model.SelectedElementName = elementSelectionView.ElementsDropDown.Selected;
		}

		private void Test(string elementName)
		{
			Skyline.DataMiner.Automation.Element element = engine.FindElement(elementName);
			var parameters = element.Protocol.Parameters;
			foreach (var parameter in parameters)
			{
				engine.Log($"Parameter info: {parameter}");
			}

			var parameters2 = element.Protocol.GetAllParameters();
			foreach (var parameter2 in parameters2)
			{
				engine.Log($"Parameter2 info: {parameter2}");
			}

			var test = model.Parameters;
			foreach (var x in test)
			{
				engine.Log($"Key: {x.Key} | Value: {x.Value}");
			}
		}
	}
}
