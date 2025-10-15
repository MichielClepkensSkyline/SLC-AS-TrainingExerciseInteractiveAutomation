using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1
{
    public class ExceptionDialog
    {
        private readonly IEngine _engine;
        private readonly InteractiveController _app;

        public ExceptionDialog(IEngine engine, InteractiveController app)
        {
            _engine=engine;
            _app=app;
        }

        public void Check(bool checkIfItExists, Dialog currentView, Dialog nextView, string message)
        {
            if (checkIfItExists)
            {
                _app.ShowDialog(nextView);
            }
            else
            {
                var messageBox = new MessageDialog(_engine, message);
                _app.ShowDialog(messageBox);

                messageBox.OkButton.Pressed += (s, e) =>
                {
                    _app.ShowDialog(currentView);
                };
            }
        }
    }
}