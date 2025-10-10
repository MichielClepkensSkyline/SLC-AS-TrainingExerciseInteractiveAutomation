using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAutomation.Wizard.ParameterSelection
{
	internal interface IParameterSelectionView
	{
		Button NextButton { get; }
	}
}
