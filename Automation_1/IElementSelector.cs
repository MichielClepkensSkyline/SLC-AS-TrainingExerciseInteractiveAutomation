using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyline.DataMiner.Core.DataMinerSystem.Common;

namespace Automation_1
{
    public interface IElementSelector
    {
        IReadOnlyCollection<IDmsElement> Elements { get; }

        IDmsElement SelectedElement { get; set; }

        int SelectedParameter { get; set; }

        string StringValue { get; set; }

        double DoubleValue { get; set; }
    }
}
