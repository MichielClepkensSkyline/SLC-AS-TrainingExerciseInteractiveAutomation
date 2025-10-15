namespace InteractiveAutomation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;

	internal class Model : IModel
	{
		private readonly IEngine engine;
		private readonly IDms dms;

		private IDictionary<string, IDmsElement> elements;
		private string selectedElementName;

		// private object selectedParameter; // TODO hoe dit doen?
		private int selectedParameterId;

		public Model(IDms dms, IEngine engine)
		{
			this.dms = dms ?? throw new ArgumentNullException(nameof(dms));
			this.engine = engine;
		}

		public IDictionary<string, IDmsElement> Elements
		{
			get
			{
				elements = dms.GetElements()
					.Where(element => element.State == Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active)
					.ToDictionary(element => element.Name);
				return elements;
			}
		}

		public string SelectedElementName
		{
			get
			{
				return selectedElementName ?? null; // TODO wat invullen indien er geen element geselecteerd is?
			}

			set
			{
				engine.Log($"The selected element is: {value}");
				if (value == selectedElementName)
				{
					return;
				}

				selectedElementName = value;

				// selectedParameter = null;
			}
		}

		public IDictionary<int, string> Parameters
		{
			get
			{
				Element element = engine.FindElement(SelectedElementName);
				if (element != null)
				{
					var parameters = element.Protocol.GetAllParameters();
					if (parameters != null)
					{
						/*foreach (var parameter in parameters)
						{
							engine.Log($"---------{parameter.Name}---------");
							engine.Log($"IsDynamicData: {parameter.IsDynamicData}");
							engine.Log($"WriteType: {parameter.WriteType}");
							engine.Log($"ArrayType: {parameter.ArrayType}");
							engine.Log($"Category: {parameter.Category}");
							engine.Log($"ComponentInfo: {parameter.ComponentInfo}");
							engine.Log($"DynamicUnits: {parameter.DynamicUnits}");
							engine.Log($"FixedType: {parameter.FixedType}");
							engine.Log($"GetType(): {parameter.GetType()}");
							engine.Log($"IsDiscreet: {parameter.IsDiscreet}");
							engine.Log($"IsDouble: {parameter.IsDouble}");
							engine.Log($"IsString: {parameter.IsString}");
							engine.Log($"IsTable: {parameter.IsTable}");
							engine.Log($"IsTableColumn: {parameter.IsTableColumn}");
							engine.Log($"ParameterType: {parameter.ParameterType}");
						}*/

						return parameters
							.Where(parameter =>
								parameter.ID < 16000 &&
								parameter.ParameterType != ParameterMeasurementType.Title &&
								!parameter.IsTable &&
								!parameter.IsTableColumn &&
								!parameter.WriteType)
							.ToDictionary(parameter => parameter.ID, parameter => parameter.DisplayName);
					}
				}

				return null;
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
			IDmsElement selectedElement = dms.GetElement(selectedElementName);
			if (selectedElement != null && selectedElement.State == Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active)
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

		public string SetDoubleOnParameter(double value)
		{
			IDmsElement selectedElement = dms.GetElement(selectedElementName);
			if (selectedElement != null && selectedElement.State == Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active)
			{
				try
				{
					// double? test = 55;
					IDmsStandaloneParameter<double?> parameter = selectedElement.GetStandaloneParameter<double?>(selectedParameterId);
					parameter.SetValue(value);
					return "Success";
				}
				catch (Exception e)
				{
					engine.Log($"{e}");
					return Convert.ToString(e);
				}
			}

			return "The selected element is not valid";
		}
	}
}
