using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyline.DataMiner.Core.DataMinerSystem.Common;
using Skyline.DataMiner.Net.Messages;

namespace Automation_1
{
    public interface IElementSelector
    {
        IReadOnlyCollection<IDmsElement> Elements { get; }

        IDmsElement SelectedElement { get; set; }

        IEnumerable<ParameterInfo> Parameters { get; }

        int SelectedParameter { get; set; }

        string StringValue { get; set; }

        double DoubleValue { get; set; }
    }
}
