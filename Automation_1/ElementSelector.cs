namespace Automation_1
{
    using System.Collections.Generic;
    using System.Linq;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Core.DataMinerSystem.Common;
    using Skyline.DataMiner.Net.Messages;

    public class ElementSelector : IElementSelector
    {
        private readonly IEngine _engine;
        private readonly IDms dms;

        private List<IDmsElement> elements;
        private IDmsElement selectedElement;
        private IEnumerable<ParameterInfo> parameters;
        private int selectedParameter;
        private string stringValue;
        private double doubleValue;

        public ElementSelector(IEngine engine, IDms dms)
        {
            _engine= engine;
            this.dms=dms;
        }

        public IReadOnlyCollection<IDmsElement> Elements
        {
            get { return elements ?? (elements = dms.GetElements().ToList()); }
        }

        public IDmsElement SelectedElement
        {
			get
			{
				return selectedElement ?? (selectedElement = Elements.First());
			}

			set
			{
				if (value == selectedElement)
					return;
				selectedElement = value;
			}
        }

        public int SelectedParameter
        {
			get
			{
				return selectedParameter;
			}

			set
			{
                if (value == selectedParameter)
					return;
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

        public IEnumerable<ParameterInfo> Parameters
        {
            get
            {
                Element element = _engine.FindElement(SelectedElement.AgentId, SelectedElement.Id);
                var parameterInfos = element.Protocol.GetAllParameters().Where(p => p.ID < 63999 && p.IsTable == false);
                parameters = parameterInfos;
                return parameters;
            }
        }
    }
}
