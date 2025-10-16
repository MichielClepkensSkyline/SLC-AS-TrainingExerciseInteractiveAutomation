using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterSelection
{
    public class ParameterSelectionView : Dialog, IParameterSelectionView
    {
        public ParameterSelectionView(IEngine engine) : base(engine)
        {
            ParameterIdDropDown = new DropDown { IsSorted = true, IsDisplayFilterShown = true };

            NextButton = new Button("Next");
            BackButton = new Button("Back");

            AddWidget(new Label("Parameter ID"), 0, 0);
            AddWidget(ParameterIdDropDown, 0, 1);
            AddWidget(BackButton, 1, 0);
            AddWidget(NextButton, 1, 1);
        }

        public DropDown ParameterIdDropDown { get; }

        public Button NextButton { get; }

        public Button BackButton { get; }
    }
}
