using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection
{
    public class ParameterValueSelectionView: Dialog, IParameterValueSelectionView
    {
        public ParameterValueSelectionView(IEngine engine) : base(engine)
        {

            StringValueLabel = new Label("String Value");

            StringValueTextBox = new TextBox
            {
                Width = 100,
            };
            SetStringVauleButton = new Button("Set Value");

            DoubleValueLabel = new Label("Double Value");
            DoubleValueNumeric = new Numeric
            {
                StepSize = 0.01,
                Decimals = 2,
                Width = 100,
            };
            SetDoubleValueButton = new Button("Set Value");

            ExceptionTextBox = new TextBox
            {
                IsMultiline = true,
                IsReadOnly = true,
                Width = 100,
                IsEnabled = false,
            };

            ExitButton = new Button("Exit");
            BackButton = new Button("Back");

            AddWidget(StringValueLabel, 0, 0);
            AddWidget(StringValueTextBox, 0, 1);
            AddWidget(SetStringVauleButton, 0, 2);

            AddWidget(DoubleValueLabel, 1, 0);
            AddWidget(DoubleValueNumeric, 1, 1);
            AddWidget(SetDoubleValueButton, 1, 2);

            AddWidget(ExceptionTextBox, 2, 0, 1, 1);

            AddWidget(BackButton, 3, 0);
            AddWidget(ExitButton, 3, 1);
        }

        public Label StringValueLabel { get; }

        public Label DoubleValueLabel { get; }

        public TextBox StringValueTextBox { get; }

        public Button SetStringVauleButton { get; }

        public Numeric DoubleValueNumeric { get; }

        public Button SetDoubleValueButton { get; }

        public TextBox ExceptionTextBox { get; }

        public Button BackButton { get; }

        public Button ExitButton { get; }
    }
}
