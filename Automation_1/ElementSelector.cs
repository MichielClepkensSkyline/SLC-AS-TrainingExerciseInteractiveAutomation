using Skyline.DataMiner.Core.DataMinerSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1
{
    public class ElementSelector : IElementSelector
    {
        private readonly IDms dms;

        private List<IDmsElement> elements;
        private IDmsElement selectedElement;
        private int selectedParameter;
        private string stringValue;
        private double doubleValue;

        public ElementSelector(IDms dms)
        {
            this.dms=dms;
        }

        public IReadOnlyCollection<IDmsElement> Elements
        {
            get { return elements ?? (elements = dms.GetElements().ToList()); }
        }

        public IDmsElement SelectedElement
        {
            get { return selectedElement ?? (selectedElement = Elements.First()); }

            set
            {
                if (value == selectedElement) return;

                selectedElement = value;
            }
        }

        public int SelectedParameter
        {
            get { return selectedParameter; }

            set
            {
                if (value == selectedParameter) return;
                selectedParameter = value;
            }
        }

        public string StringValue
        {
            get
            {
                return stringValue;
            }

            set
            {
                stringValue = value;
            }
        }

        public double DoubleValue
        {
            get
            {
                return doubleValue;
            }

            set
            {
                doubleValue = value;
            }
        }
    }
}
