namespace InteractiveAutomation
{
	using System.Collections.Generic;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;

	public interface IModel
	{
		IDictionary<string, IDmsElement> Elements { get; }

		IDictionary<int, ParameterInfo> Parameters { get; }

		string SelectedElementName { get; set; }

		int SelectedParameterId { get; set; }

		string SetStringOnParameter(string value);

		string SetDoubleOnParameter(double value);
	}
}
