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
	using ElementState = Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState;

	public class Model : IModel
	{
		private const int MaxParameterId = 64000;

		private readonly IEngine engine;
		private readonly IDms dms;

		private IDictionary<string, IDmsElement> elements;
		private IDictionary<int, ParameterInfo> parameters;

		private string selectedElementName;
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
					.Where(element => element.State == ElementState.Active)
					.ToDictionary(element => element.Name) ?? new Dictionary<string, IDmsElement>();
				return elements;
			}
		}

		public string SelectedElementName
		{
			get
			{
				return selectedElementName ?? elements.First().Key;
			}

			set
			{
				if (value == selectedElementName)
				{
					return;
				}

				selectedElementName = value;
			}
		}

		public IDictionary<int, ParameterInfo> Parameters
		{
			get
			{
				Element element = engine.FindElement(SelectedElementName);
				if (element != null && element.IsActive)
				{
					var parameters = element.Protocol.GetAllParameters();
					if (parameters != null)
					{
						this.parameters = parameters
							.Where(parameter =>
								parameter.ID < MaxParameterId &&
								parameter.ParameterType != ParameterMeasurementType.Title &&
								!parameter.IsTable &&
								!parameter.IsTableColumn &&
								!parameter.WriteType)
							.ToDictionary(parameter => parameter.ID) ?? new Dictionary<int, ParameterInfo>();
						return this.parameters;
					}
				}

				return null;
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

		public string SetStringOnParameter(string value)
		{
			if (!String.IsNullOrWhiteSpace(value))
			{
				Element selectedElement = engine.FindElement(SelectedElementName);
				if (selectedElement != null && selectedElement.IsActive)
				{
					if (CheckParameterExists(selectedElement, SelectedParameterId))
					{
						ParameterInfo parameter = selectedElement.Protocol.FindParameter(SelectedParameterId);
						if (CheckParameter(parameter) && parameter.IsString)
						{
							try
							{
								selectedElement.SetParameter(SelectedParameterId, value);
								return "Success";
							}
							catch (Exception e)
							{
								return Convert.ToString(e);
							}
						}
						else
						{
							return GenerateReturnMessage(parameter, parameter.IsString, "string");
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
			else
			{
				return "Value can't be null or whitespace";
			}
		}

		public string SetDoubleOnParameter(double value)
		{
			Element selectedElement = engine.FindElement(SelectedElementName);
			if (selectedElement != null && selectedElement.IsActive)
			{
				if (CheckParameterExists(selectedElement, SelectedParameterId))
				{
					ParameterInfo parameter = selectedElement.Protocol.FindParameter(SelectedParameterId);
					if (CheckParameter(parameter) && parameter.IsDouble)
					{
						try
						{
							selectedElement.SetParameter(SelectedParameterId, value);
							return "Success";
						}
						catch (Exception e)
						{
							return Convert.ToString(e);
						}
					}
					else
					{
						return GenerateReturnMessage(parameter, parameter.IsDouble, "double");
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

		private static string GenerateReturnMessage(ParameterInfo parameter, bool typeCheck, string type)
		{
			if (!typeCheck)
			{
				return $"The selected parameter is not of type {type}";
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

		private static bool CheckParameterExists(Element element, int parameterId)
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

		private static bool CheckParameter(ParameterInfo parameter)
		{
			return parameter.ID < MaxParameterId &&
				!parameter.IsTable &&
				!parameter.IsTableColumn &&
				!parameter.WriteType &&
				parameter.ParameterType != ParameterMeasurementType.Title;
		}
	}
}
