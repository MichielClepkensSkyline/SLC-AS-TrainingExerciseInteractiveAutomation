namespace Automation_1
{
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

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