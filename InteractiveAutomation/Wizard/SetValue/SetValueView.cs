using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAutomation.Wizard.SetValue
{
	internal class SetValueView : Dialog , ISetValueView
	{
		private readonly Label elementLabel;

		public SetValueView(IEngine engine) : base(engine)
		{
			// Alles wat op de UI komt aanmaken (komt allemaal uit de INteractiveAutomationScript package)
			elementLabel = new Label($"Value: ");
			ValueBox = new TextBox();
			FinishButton = new Button($"Finish");

			Title = $"Select your value:";

			// Toevoegen van de UI components aan een bepaalde plek in de UI
			AddWidget(elementLabel, 1, 0);
			AddWidget(ValueBox, 1, 1);
			AddWidget(FinishButton, 2, 1);
		}

		public Button FinishButton { get; private set; }

		public TextBox ValueBox { get; }
	}
}
