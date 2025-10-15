namespace InteractiveAutomation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;

	internal interface IModel
	{
		IDictionary<string, IDmsElement> Elements { get; }

		IDictionary<int, ParameterInfo> Parameters { get; }

		string SelectedElementName { get; set; }

		int SelectedParameterId { get; set; }

		string SetStringOnParameter(string value);

		string SetDoubleOnParameter(double value);
	}
}
