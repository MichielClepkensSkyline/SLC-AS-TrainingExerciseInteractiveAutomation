namespace Automation_1
{
    using System.Collections.Generic;
    using Skyline.DataMiner.Core.DataMinerSystem.Common;
    using Skyline.DataMiner.Net.Messages;

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
