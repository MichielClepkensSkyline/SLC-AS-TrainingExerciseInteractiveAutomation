namespace InteractiveAutomation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	internal interface IModel
	{
		IDictionary<string, IDmsElement> Elements { get; }

		string SelectedElementName { get; set; }

		int SelectedParameterId { get; set; }

		string SetStringOnParameter(string value);

		string SetDoubleOnParameter(double value);
	}
}
