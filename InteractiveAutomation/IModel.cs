namespace InteractiveAutomation
{
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	internal interface IModel
	{
		ICollection<IDmsElement> Elements { get; }

		IDmsElement SelectedElement { get; set; }

		int SelectedParameterId { get; set; }
	}
}
