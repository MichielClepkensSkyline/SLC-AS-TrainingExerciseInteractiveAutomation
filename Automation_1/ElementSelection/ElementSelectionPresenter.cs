using Skyline.DataMiner.Core.DataMinerSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ElementSelection
{
    public class ElementSelectionPresenter
    {
        public bool isElementActive;
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

        private void StoreToModel()
        {
            string selected = _view.ElementDropDown.Selected;
            _model.SelectedElement = _elementsByName[selected];
            if (_model.SelectedElement.State!= Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active)
            {
                isElementActive = false;
            }
            else
            {
                isElementActive = true;
            }
        }

        public void OnNextButtonPressed(object sender, EventArgs e)
        {
            StoreToModel();

            Next?.Invoke(this, EventArgs.Empty);
        }
    }
}
