namespace Automation_1.ElementSelection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Skyline.DataMiner.Core.DataMinerSystem.Common;

    public class ElementSelectionPresenter
    {
        public bool IsElementActive;
        private readonly IElementSelectionView _view;
        private readonly IElementSelector _model;

        public Dictionary<string, IDmsElement> _elementsByName;

        public ElementSelectionPresenter(IElementSelector model, IElementSelectionView view)
        {
            _model=model ?? throw new ArgumentNullException(nameof(view));
            _view=view ?? throw new ArgumentNullException(nameof(model));

            view.NextButton.Pressed += OnNextButtonPressed;
        }

        public event EventHandler<EventArgs> Next;

        public void LoadFromModel()
        {
            _elementsByName= _model.Elements.ToDictionary(element => element.Name);

            _view.ElementDropDown.SetOptions(_elementsByName.Keys);
            _view.ElementDropDown.Selected = _model.SelectedElement.Name;
        }

        public void StoreToModel()
        {
            string selected = _view.ElementDropDown.Selected;
            _model.SelectedElement = _elementsByName[selected];
            if (_model.SelectedElement.State!= Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active)
            {
                IsElementActive = false;
            }
            else
            {
                IsElementActive = true;
            }
        }

        public void OnNextButtonPressed(object sender, EventArgs e)
        {
            StoreToModel();

            Next?.Invoke(this, EventArgs.Empty);
        }
    }
}
