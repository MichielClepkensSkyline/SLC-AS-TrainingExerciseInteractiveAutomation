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
		private readonly IDms dms;
		private IDmsElement[] elements;
		private IDmsElement selectedElement;
		private IEngine engine;
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
				return elements ?? (elements = dms.GetElements().Where(x => x.State == Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active).ToArray());
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
			get => selectedParameterId;
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
				var parameters = element.Protocol.GetAllParameters().Where(p => p.ID < 63999 && p.IsTableColumn == false && p.IsTable == false);
				parameterInfos = parameters;
				return parameterInfos;
			}
		}
	}
}
