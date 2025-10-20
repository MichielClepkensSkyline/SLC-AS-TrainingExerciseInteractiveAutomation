using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAutomation.Wizard.SetValue
{
	internal abstract class ASetValueView : Dialog
	{

		private readonly Label elementLabel;

		protected ASetValueView(IEngine engine) : base(engine)
		{
			elementLabel = new Label("Value: ");
			AddWidget(elementLabel, 0, 0);
		}
	}
}
