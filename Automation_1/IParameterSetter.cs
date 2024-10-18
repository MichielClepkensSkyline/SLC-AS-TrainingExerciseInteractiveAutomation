namespace Automation_1
{
	using System.Collections.Generic;

	using Automation_1.Dtos;

	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public interface IParameterSetter
	{
		ICollection<IDmsElement> Elements { get; }

		IDmsElement SelectedElement { get; set; }

		ParameterInfo SelectedParameter { get; set; }

		ICollection<ParameterInfo> Parameters { get; }
	}
}
