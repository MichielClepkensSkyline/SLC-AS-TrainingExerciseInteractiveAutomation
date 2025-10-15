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
        Label StringValueLabel { get; }

        Label DoubleValueLabel { get; }

        TextBox StringValueTextBox { get; }

        Button SetStringVauleButton { get; }

        Numeric DoubleValueNumeric { get; }

        Button SetDoubleValueButton { get; }

        TextBox ExceptionTextBox { get; }

        Button BackButton { get; }

        Button ExitButton { get; }
    }
}
