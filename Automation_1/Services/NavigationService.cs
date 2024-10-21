namespace Automation_1.Services
{
    using System;

    using Automation_1.Dtos;
    using Automation_1.Enums;
    using Automation_1.Model;
    using Automation_1.Wizard.ElementSelection;
    using Automation_1.Wizard.ParameterSelection;
    using Automation_1.Wizard.ValueSelection;

    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public class NavigationService
    {
        private readonly InteractiveController _app;
        private readonly ParameterService _parameterService;

        public NavigationService(InteractiveController app, ParameterService parameterService)
        {
            _app = app;
            _parameterService = parameterService;
        }

        public ViewDto CreateViews(IEngine engine)
        {
            return new ViewDto
            {
                ElementSelectionView = new ElementSelectionView(engine),
                ParameterSelectionView = new ParameterSelectionView(engine),
                ValueSelectionView = new ValueSelectionView(engine),
            };
        }

        public PresenterDto CreatePresenters(ViewDto views, ParameterSetter parameterSetter)
        {
            return new PresenterDto
            {
                ElementSelection = new ElementSelectionPresenter(views.ElementSelectionView, parameterSetter),
                ParameterSelection = new ParameterSelectionPresenter(views.ParameterSelectionView, parameterSetter),
                ValueSelection = new ValueSelectionPresenter(views.ValueSelectionView, parameterSetter),
            };
        }

        public void HandleEvents(PresenterDto presenters, ViewDto views, IEngine engine, ParameterSetter parameterSetter)
        {
            presenters.ElementSelection.Continue += (sender, args) =>
            {
                presenters.ParameterSelection.LoadFromModel();
                _app.ShowDialog(views.ParameterSelectionView);
            };

            presenters.ParameterSelection.Back += (sender, args) =>
                _app.ShowDialog(views.ElementSelectionView);

            presenters.ParameterSelection.Continue += (sender, args) =>
            {
                GenerateInputWidget(engine, parameterSetter, views);
                presenters.ValueSelection.LoadFromModel();
                _app.ShowDialog(views.ValueSelectionView);
            };

            presenters.ValueSelection.Back += (sender, args) =>
                _app.ShowDialog(views.ParameterSelectionView);

            presenters.ValueSelection.Exit += (sender, args) =>
                _app.Stop();

            presenters.ValueSelection.Finish += (sender, args) =>
                OnFinish(engine, parameterSetter, views);
        }

        private void OnFinish(IEngine engine, ParameterSetter parameterSetter, ViewDto views)
        {
            switch (parameterSetter.SelectedParameter.Type)
            {
                case ParameterType.String:
                    _parameterService.SetParameterValue(parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, parameterSetter.NewParameterValue);
                    engine.ExitSuccess("Finish button was pressed.");
                    break;

                case ParameterType.Double:
                    if (_parameterService.TrySetNumericalParameter(parameterSetter, parameterSetter.NewParameterValue, out string feedbackMessage))
                    {
                        engine.ExitSuccess("The parameter value was set successfully.");
                    }
                    else
                    {
                        views.ValueSelectionView.SetFeedbackMessage(feedbackMessage);
                    }

                    break;

                default:
                    views.ValueSelectionView.SetFeedbackMessage("Unexpected parameter type");
                    break;
            }
        }

        private void GenerateInputWidget(IEngine engine, ParameterSetter parameterSetter, ViewDto views)
        {
            var parameterType = ParameterService.GetParameterType(engine, parameterSetter);

            Widget inputWidget;

            if (parameterType == ParameterType.DateTime)
            {
                inputWidget = new DateTimePicker();
            }
            else if (parameterType == ParameterType.Double)
            {
                inputWidget = new Numeric { Decimals = 2, StepSize = 0.01 };
            }
            else if (parameterType == ParameterType.String)
            {
                inputWidget = new TextBox
                {
                    PlaceHolder = "Enter new parameter value",
                    Width = 310,
                    IsMultiline = true,
                };
            }
            else
            {
                throw new InvalidOperationException($"Unsupported widget type: {views.ValueSelectionView.CurrentInput.GetType().Name}");
            }

            views.ValueSelectionView.SetInputWidget(inputWidget);
        }
    }
}
