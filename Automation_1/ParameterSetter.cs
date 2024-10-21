namespace Automation_1
{
	using System.Collections.Generic;
	using System.Linq;

	using Automation_1.Dtos;
	using Automation_1.Enums;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ParameterSetter : IParameterSetter
	{
		private readonly IEngine _engine;
		private readonly IDms _dms;
		private readonly IEnumerable<IDmsElement> _allElements;

		private IDmsElement _selectedElement;
		private ParameterInfo _selectedParameter;
		private double _newParameterValueNumeric;
		private string _newParameterValue;

		public ParameterSetter(IEngine engine)
		{
			_engine = engine;
			_dms = _engine.GetDms();
			_selectedElement = null;
			_allElements = _dms.GetElements().Where(element => element.State != ElementState.Stopped);
		}

		public IEnumerable<IDmsElement> Elements
		{
			get
			{
				return _allElements ?? _dms.GetElements().Where(element => element.State != ElementState.Stopped);
			}
		}

		public IDmsElement SelectedElement
		{
			get => _selectedElement;
			set => _selectedElement = value;
		}

		public ParameterInfo SelectedParameter
		{
			get => _selectedParameter;
			set => _selectedParameter = value;
		}

		public ICollection<ParameterInfo> Parameters
		{
			get
			{
				var protocolInfo = _engine.GetUserConnection().GetProtocol(SelectedElement.Protocol.Name, SelectedElement.Protocol.Version);

				var parameters = new List<ParameterInfo>();
				foreach (var parameter in protocolInfo.Parameters)
				{
					var type = GetParameterType(parameter.InterpreteType.ToString());

					if (!parameter.WriteType &&
						parameter.ID < 64000 &&
						!parameter.IsTableColumn &&
						type != ParameterType.Undef)
					{
						parameters.Add(new ParameterInfo
						{
							Id = parameter.ID,
							Name = parameter.Name,
							Type = type,
							Description = parameter.Description,
						});
					}
				}

				return parameters;
			}
		}

		public double NewParameterValueNumeric
		{
			get => _newParameterValueNumeric;
			set => _newParameterValueNumeric = value;
		}

		public string NewParameterValue
		{
			get => _newParameterValue;
			set => _newParameterValue = value;
		}

		private static ParameterType GetParameterType(string interpreteType)
		{
			switch (interpreteType.ToLower())
			{
				case "double":
					return ParameterType.Double;
				case "string":
					return ParameterType.String;
				default:
					return ParameterType.Undef;
			}
		}
	}
}
