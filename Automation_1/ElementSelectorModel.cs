namespace Automation_1
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;
	using Skyline.DataMiner.Net.ReportsAndDashboards;

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

		private bool isParameterValid;

		public event EventHandler SelectedParameterChanged;

		public bool IsParameterValid
		{
			get
			{
				return isParameterValid;
			}
		}

		public int SelectedParameterId
		{
			get => selectedParameterId;
			set
			{
				//engine.Log("Element: " + $"{SelectedElement.AgentId}/{SelectedElement.Id.ToString()}");
				/*selectedParameterId = value;
				isParameterValid = false;

				if (SelectedElement != null && selectedParameterId > 0)
				{
					try
					{
						var parameter = selectedElement.GetStandaloneParameter<string>(selectedParameterId);
						var types = parameter.GetType();
				engine.Log($"is type of {types}");
						var parameterValue = parameter.GetValue();
						isParameterValid = true;
					}
					catch
					{
						isParameterValid = false;
					}
				}

				SelectedParameterChanged?.Invoke(this, EventArgs.Empty);*/

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
				//engine.Log("Frist parameter: " + parameters.First().Name);
				parameterInfos = parameters;
				//engine.Log("Type parameter: " + parameters.ElementAt(0).InterpreteType);
				return parameterInfos;
			}
		}
	}
}
