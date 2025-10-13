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
		private readonly Label stringLabel;
		private readonly Label doubleLabel;

		public SetValueView(IEngine engine) : base(engine)
		{
			// Alles wat op de UI komt aanmaken (komt allemaal uit de INteractiveAutomationScript package)
			stringLabel = new Label($"String Value: ");
			StringValueBox = new TextBox() { Width = 150 };
			StringButton = new Button($"Set String Value") { Width = 110};

			doubleLabel = new Label($"Double Value: ");
			DoubleValueBox = new Numeric() { Decimals = 2, StepSize = 0.01, Width = 150 };
			DoubleButton = new Button($"Set Double Value") { Width = 110};

			MessageBox = new TextBox() { IsReadOnly = true, IsMultiline = true };

			FinishButton = new Button($"Exit");
			BackButton = new Button($"Back...");

			Title = $"Set value";

			// Toevoegen van de UI components aan een bepaalde plek in de UI
			AddWidget(stringLabel, 0, 0);
			AddWidget(StringValueBox, 0, 1);
			AddWidget(StringButton, 0, 2);
			AddWidget(doubleLabel, 1, 0);
			AddWidget(DoubleValueBox, 1, 1);
			AddWidget(DoubleButton, 1, 2);
			AddWidget(MessageBox, 2, 0, 1, 3);
			AddWidget(BackButton, 3, 0);
			AddWidget(FinishButton, 3, 1);
		}

		public TextBox StringValueBox { get; private set; }

		public Button StringButton { get; private set; }

		public Numeric DoubleValueBox { get; private set; }

		public Button DoubleButton { get; private set; }

		public TextBox MessageBox { get; private set; }

		public Button FinishButton { get; private set; }

		public Button BackButton { get; private set; }
	}
}
