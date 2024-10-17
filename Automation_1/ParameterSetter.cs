namespace Automation_1
{
	using System.Collections.Generic;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ParameterSetter : IParameterSetter
	{
		private readonly IDms _dms;
		private readonly ICollection<IDmsElement> _allElements;

		private IDmsElement _selectedElement;

		public ParameterSetter(IDms dms)
		{
			_dms = dms;
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
	}
}
