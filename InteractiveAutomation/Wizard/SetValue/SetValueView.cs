namespace InteractiveAutomation.Wizard.SetValue
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class SetValueView : Dialog , ISetValueView
	{
		private readonly Label elementLabel;

		public SetValueView(IEngine engine) : base(engine)
		{
			// Alles wat op de UI komt aanmaken (komt allemaal uit de INteractiveAutomationScript package)
			elementLabel = new Label($"Value: ");
			ValueBox = new TextBox();
			FinishButton = new Button($"Finish");
			BackButton = new Button($"Back");

			Title = $"Select your value:";

			// Toevoegen van de UI components aan een bepaalde plek in de UI
			AddWidget(elementLabel, 0, 0);
			AddWidget(ValueBox, 0, 1);
			AddWidget(BackButton, 1, 0);
			AddWidget(FinishButton, 1, 1);
		}

		public Button FinishButton { get; private set; }

		public Button BackButton { get; private set; }

		public TextBox ValueBox { get; }
	}
}
