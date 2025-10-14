using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection
{
	public class ParameterValueSelectionView : Dialog
	{
		public ParameterValueSelectionView(IEngine engine) : base(engine)
		{
			Title = "Set parameter value";
			BackButton = new Button("Back");
			SetStringValue = new Button("Set String Value");
			SetDoubleValue = new Button("Set Double Value");
			StringValue = new TextBox();
			DoubleValue = new Numeric() { Decimals = 2, StepSize= 0.01 };
			Message = new TextBox() { IsMultiline = true, IsEnabled = false };

			AddWidget(StringValue, 0, 0);
			AddWidget(SetStringValue, 0, 1);
			AddWidget(DoubleValue, 1, 0);
			AddWidget(SetDoubleValue, 1, 1);
			AddWidget(BackButton, 2, 0);
			AddWidget(Message, 3, 0, 1, 3);
		}

		public Button BackButton { get; }

		public TextBox StringValue { get; }

		public Numeric DoubleValue { get; }

		public Button SetStringValue { get; }

		public Button SetDoubleValue { get; }

		public TextBox Message { get; set; }
	}
}
