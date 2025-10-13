namespace InteractiveAutomation
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	internal class Model : IModel
	{
		private readonly IEngine engine;
		private readonly IDms dms;

		private List<IDmsElement> elements;
		private IDmsElement selectedElement;
		// private object selectedParameter; // TODO hoe dit doen?
		private int selectedParameterId;

		public Model(IDms dms, IEngine engine)
		{
			this.dms = dms ?? throw new ArgumentNullException(nameof(dms));
			this.engine = engine;
		}

		public IDmsElement SelectedElement
		{
			get
			{
				return selectedElement ?? null; // TODO wat invullen indien er geen element geselecteerd is?
			}

			set
			{
				engine.Log($"The selected element is: {value.Name}");
				if (value == selectedElement)
				{
					return;
				}

				selectedElement = value;
				// selectedParameter = null;
			}
		}

		// object SelectedParameter

		public ICollection<IDmsElement> Elements
		{
			get
			{
				elements = (List<IDmsElement>)(elements ?? dms.GetElements());
				return elements;
			}
		}

		public int SelectedParameterId
		{
			get
			{
				return selectedParameterId; // TODO moet er hier een default value?
			}

			set
			{
				engine.Log($"The selected parameter id is: {value}");
				if (value == selectedParameterId)
				{
					return;
				}

				selectedParameterId = value;
			}
		}

		public string SetStringOnParameter(string value)
		{
			if (selectedElement != null && selectedElement.State == ElementState.Active)
			{
				try
				{
					// How to set on an element
					// selectedElement.
					IDmsStandaloneParameter<string> parameter = selectedElement.GetStandaloneParameter<string>(selectedParameterId);
					parameter.SetValue(value);
					return "Success";
				}
				catch (Exception e)
				{
					// Fill exception into the textbox
					engine.Log(Convert.ToString(e));
					return Convert.ToString(e);
				}
			}

			return "The selected element is not valid";
		}
	}
}
