namespace Automation_1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;

	public interface IElementSelector
	{
		event EventHandler SelectedParameterChanged;

		IReadOnlyCollection<IDmsElement> Elements { get; }

		IDmsElement SelectedElement { get; set; }

		int SelectedParameterId { get; set; }

		string SetParameterValueString { get; set; }

		double SetParameterValueDouble { get; set; }

		bool IsParameterValid { get; }

		IEnumerable<ParameterInfo> Parameters { get; }
	}
}
