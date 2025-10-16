using Skyline.DataMiner.Utils.InteractiveAutomationScript;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterSelection
{
	public interface IParameterSelectionView
	{
		Numeric ParameterId { get; }

		Button ContinueButton { get; }

		Button BackButton { get; }

		Label SelectParameterLabel { get; }
	}
}
