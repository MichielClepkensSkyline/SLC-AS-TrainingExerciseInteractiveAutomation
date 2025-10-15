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
        Button NextButton { get; }

        DropDown ElementDropDown { get; }
    }
}
