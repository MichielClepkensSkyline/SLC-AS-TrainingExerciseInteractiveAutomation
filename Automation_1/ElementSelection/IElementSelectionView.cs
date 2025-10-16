using Skyline.DataMiner.Utils.InteractiveAutomationScript;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ElementSelection
{
	public interface IElementSelectionView
	{
		Label SetParameterLabel { get; set; }

		DropDown ElementsDropDown { get; }

		Button ContinueButton { get; }
	}
}
