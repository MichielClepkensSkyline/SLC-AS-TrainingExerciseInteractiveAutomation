namespace Automation_1
{
	using System.Collections.Generic;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public interface IParameterSetter
	{
		ICollection<IDmsElement> Elements { get; }

		IDmsElement SelectedElement { get; set; }
	}
}
