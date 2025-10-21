namespace InteractiveAutomation.Wizard.ElementSelection
{
	using System;
	using System.Linq;
	using Skyline.DataMiner.Automation;

	internal class ElementSelectionPresenter
	{
		private readonly IElementSelectionView elementSelectionView;
		private readonly IModel model;

		public ElementSelectionPresenter(IElementSelectionView view, IModel model)
		{
			this.elementSelectionView = view ?? throw new ArgumentNullException(nameof(view));
			this.model = model ?? throw new ArgumentNullException(nameof(model));

			elementSelectionView.NextButton.Pressed += OnNextPressed;
		}

		public event EventHandler<EventArgs> Next;

		public void LoadFromModel()
		{
			if (model.Elements != null && model.Elements.Any())
			{
				// Set elements to DropDown
				elementSelectionView.ElementsDropDown.SetOptions(model.Elements.Keys);
			}
		}

		private void OnNextPressed(object sender, EventArgs e)
		{
			StoreToModel();
			Next?.Invoke(this, EventArgs.Empty);
		}

		private void StoreToModel()
		{
			model.SelectedElementName = elementSelectionView.ElementsDropDown.Selected;
		}
	}
}
