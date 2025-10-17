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
        IDropDown ParameterIdDropDown { get; }

        Button NextButton { get; }

        Button BackButton { get; }
    }
}
