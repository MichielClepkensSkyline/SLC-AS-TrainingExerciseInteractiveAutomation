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
	using Skyline.DataMiner.Net.ReportsAndDashboards;

	public class ElementSelectorModel : IElementSelector
	{
		private readonly IDms dms;
		private IDmsElement[] elements;
		private IDmsElement selectedElement;
		private IEngine engine;

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
				return elements ?? (elements = dms.GetElements().Where(x => x.State == ElementState.Active).ToArray());
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
				selectedParameterId = value;
				engine.Log("Selected Element is" + SelectedElement.Name);
				engine.Log("Selected Parameter Id is" + selectedParameterId);
				Element element = engine.FindElementByKey(SelectedElement.Id.ToString());
				isParameterValid = false;

				if (SelectedElement != null && selectedParameterId > 0)
				{
					try
					{
						engine.Log("LogLine -----------------------------");
						var parameter = selectedElement.GetStandaloneParameter<string>(selectedParameterId);
						var parameterValue = parameter.GetValue();
						isParameterValid = true;
					}
					catch
					{
						isParameterValid = false;
					}
				}

				SelectedParameterChanged?.Invoke(this, EventArgs.Empty);
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


	}
}
