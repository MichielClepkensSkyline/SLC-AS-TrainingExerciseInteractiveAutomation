using Skyline.DataMiner.Utils.InteractiveAutomationScript;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection
{
	public interface IParameterValueSelectionView
	{
		Button BackButton { get; }

		TextBox StringValue { get; }

		Numeric DoubleValue { get; }

		Button SetStringValue { get; }

		Button SetDoubleValue { get; }

		TextBox Message { get; set; }

		Label StringValueLabel { get; }

		Label DoubleValueLabel { get; }
	}
}
