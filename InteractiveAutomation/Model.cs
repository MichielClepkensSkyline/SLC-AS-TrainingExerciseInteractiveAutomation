namespace InteractiveAutomation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Exceptions;
	using Skyline.DataMiner.Net.Messages;

	internal class Model : IModel
	{
		private const int MaxParameterId = 64000;

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

		public IDictionary<int, ParameterInfo> Parameters
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
								parameter.ID < MaxParameterId &&
								parameter.ParameterType != ParameterMeasurementType.Title &&
								!parameter.IsTable &&
								!parameter.IsTableColumn &&
								!parameter.WriteType)
							.ToDictionary(parameter => parameter.ID);
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
			Element selectedElement = engine.FindElement(selectedElementName);
			if (selectedElement != null && selectedElement.IsActive)
			{
				if (CheckParameterExists(selectedElement, selectedParameterId))
				{
					ParameterInfo parameter = selectedElement.Protocol.FindParameter(selectedParameterId);
					if (CheckParameter(parameter) && parameter.IsString)
					{
						try
						{
							selectedElement.SetParameter(selectedParameterId, value);
							return "Success";
						}
						catch (Exception e)
						{
							engine.Log($"{e}");
							return Convert.ToString(e);
						}
					}
					else if (!parameter.IsString)
					{
						return "The selected parameter is not of type string";
					}
					else if (parameter.IsTable || parameter.IsTableColumn)
					{
						return "The selected parameter is part of a table";
					}
					else if (parameter.WriteType)
					{
						return "The selected parameter is of type write";
					}
					else if (parameter.ParameterType != ParameterMeasurementType.Title)
					{
						return "The selected parameter is a title";
					}
					else
					{
						return "The selected parameter is out of range";
					}
				}
				else
				{
					return "The selected parameter does not exist";
				}
			}
			else if (selectedElement == null)
			{
				return "The selected element doesn't exist";
			}
			else
			{
				return "The selected element is not active";
			}
		}

		public string SetDoubleOnParameter(double value)
		{
			Element selectedElement = engine.FindElement(selectedElementName);
			if (selectedElement != null && selectedElement.IsActive)
			{
				if (CheckParameterExists(selectedElement, selectedParameterId))
				{
					ParameterInfo parameter = selectedElement.Protocol.FindParameter(selectedParameterId);
					if (CheckParameter(parameter) && parameter.IsDouble)
					{
						try
						{
							selectedElement.SetParameter(selectedParameterId, value);
							return "Success";
						}
						catch (Exception e)
						{
							engine.Log($"{e}");
							return Convert.ToString(e);
						}
					}
					else if (!parameter.IsDouble)
					{
						return "The selected parameter is not of type double";
					}
					else if (parameter.IsTable || parameter.IsTableColumn)
					{
						return "The selected parameter is part of a table";
					}
					else if (parameter.WriteType)
					{
						return "The selected parameter is of type write";
					}
					else if (parameter.ParameterType != ParameterMeasurementType.Title)
					{
						return "The selected parameter is a title";
					}
					else
					{
						return "The selected parameter is out of range";
					}
				}
				else
				{
					return "The selected parameter does not exist";
				}
			}
			else if (selectedElement == null)
			{
				return "The selected element doesn't exist";
			}
			else
			{
				return "The selected element is not active";
			}
		}

		private bool CheckParameterExists(Element element, int parameterId)
		{
			try
			{
				element.GetParameter(parameterId);
				return true;
			}
			catch (DataMinerException)
			{
				return false;
			}
		}

		private bool CheckParameter(ParameterInfo parameter)
		{
			return parameter.ID < MaxParameterId &&
				!parameter.IsTable &&
				!parameter.IsTableColumn &&
				!parameter.WriteType &&
				parameter.ParameterType != ParameterMeasurementType.Title;
		}
	}
}
