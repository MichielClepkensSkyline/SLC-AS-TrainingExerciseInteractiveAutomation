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
		ICollection<IDmsElement> Elements { get; }

		IDmsElement SelectedElement { get; set; }

		int SelectedParameterId { get; set; }

		string SetStringOnParameter(string value);

		string SetDoubleOnParameter(double value);
	}
}
