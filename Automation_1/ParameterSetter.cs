namespace Automation_1
{
	using System.Collections.Generic;

	using Automation_1.Dtos;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ParameterSetter : IParameterSetter
	{
		private readonly IEngine _engine;
		private readonly IDms _dms;
		private readonly ICollection<IDmsElement> _allElements;
		private readonly ICollection<IDmsStandaloneParameter> _selectedElementParameters;

		private IDmsElement _selectedElement;
		private ParameterInfo _selectedParameter;

		public ParameterSetter(IEngine engine)
		{
			_engine = engine;
			_dms = _engine.GetDms();
			_selectedElement = null;
			_allElements = _dms.GetElements();
		}

		public ICollection<IDmsElement> Elements
		{
			get
			{
				return _allElements ?? _dms.GetElements();
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
					parameters.Add(new ParameterInfo
					{
						Id = parameter.ID,
						Name = parameter.Name,
					});
				}

				return parameters;
			}
		}
	}
}
