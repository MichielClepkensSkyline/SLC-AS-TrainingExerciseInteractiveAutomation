namespace InteractiveAutomation
{
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;
	using System.Collections.Generic;

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
