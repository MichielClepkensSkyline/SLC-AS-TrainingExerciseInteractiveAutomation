namespace Automation_1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;

	public class ElementSelectorModel : IElementSelector
	{
		private const int ParameterIdRange = 64000;
		private readonly IDms dms;
		private readonly IEngine engine;
		private IDmsElement[] elements;
		private IDmsElement selectedElement;
		private IEnumerable<ParameterInfo> parameterInfos;

		private int selectedParameterId;
		private string setParameterValueString;
		private double setParameterValueDouble;

		public ElementSelectorModel(IDms dms, IEngine engine)
		{
			this.engine = engine;
			if (dms == null)
			{
				throw new ArgumentNullException(nameof(dms));
			}

			this.dms = dms;
		}

		public IReadOnlyCollection<IDmsElement> Elements
		{
			get
			{
				elements = dms.GetElements().Where(element => element.State == Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active && element != null && !string.IsNullOrWhiteSpace(element.Name)).ToArray();
				return elements;
			}
		}

		public IDmsElement SelectedElement
		{
			get
			{
				return selectedElement ?? (selectedElement = Elements.First());
			}

			set
			{
				if (value == SelectedElement)
				{
					return;
				}

				selectedElement = value;
			}
		}

		public int SelectedParameterId
		{
			get
			{
				return selectedParameterId;
			}

			set
			{
				if (value == selectedParameterId)
				{
					return;
				}

				selectedParameterId = value;
			}
		}

		public string SetParameterValueString
		{
			get
			{
				return setParameterValueString;
			}

			set
			{
				setParameterValueString = value;
			}
		}

		public double SetParameterValueDouble
		{
			get
			{
				return setParameterValueDouble;
			}

			set
			{
				setParameterValueDouble = value;
			}
		}

		public IEnumerable<ParameterInfo> Parameters
		{
			get
			{
				Element element = engine.FindElement(SelectedElement.AgentId, SelectedElement.Id);
				var parameters = element.Protocol.GetAllParameters().Where(parameter => parameter.ID < ParameterIdRange && parameter.IsTableColumn == false && parameter.IsTable == false && parameter.WriteType == false && parameter.ParameterType != ParameterMeasurementType.Title);
				parameterInfos = parameters;
				return parameterInfos;
			}
		}
	}
}
