namespace Automation_1
{
	using Automation_1.Dtos;
	using Automation_1.Enums;
	using Automation_1.Wizard.ElementSelection;
	using Automation_1.Wizard.ParameterSelection;
	using Automation_1.Wizard.ValueSelection;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.ReportsAndDashboards;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class AppNavigator
	{
		private readonly InteractiveController _app;

		public AppNavigator(InteractiveController app)
		{
			_app = app;
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
				_app.ShowDialog(views.ValueSelectionView);

			presenters.ValueSelection.Back += (sender, args) =>
				_app.ShowDialog(views.ParameterSelectionView);

			presenters.ValueSelection.Exit += (sender, args) =>
				_app.Stop();

			presenters.ValueSelection.Finish += (sender, args) =>
				OnFinish(engine, parameterSetter, views);
		}

		private static void SetParameterValue<T>(IEngine engine, IDmsElement element, int parameterId, T value)
		{
			engine.GetDms()
				.GetAgent(element.AgentId)
				.GetElement(element.Name)
				.GetStandaloneParameter<T>(parameterId)
				.SetValue(value);
		}

		private static void OnFinish(IEngine engine, ParameterSetter parameterSetter, ViewDto views)
		{
			engine.GenerateInformation(
				$"INFORMATION: {parameterSetter.SelectedElement.Name} / {parameterSetter.SelectedParameter.Name} / " +
				$"{parameterSetter.SelectedParameter.Type} / {parameterSetter.NewParameterValue}");

			switch (parameterSetter.SelectedParameter.Type)
			{
				case ParameterType.String:
					SetParameterValue(engine, parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, parameterSetter.NewParameterValue);
					engine.ExitSuccess("Finish button was pressed.");
					break;

				case ParameterType.Double:
					if (double.TryParse(parameterSetter.NewParameterValue, out double parsedDouble))
					{
						SetParameterValue(engine, parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, (double?)parsedDouble);
						engine.ExitSuccess("Finish button was pressed.");
					}
					else
					{
						views.ValueSelectionView.SetFeedbackMessage($"Element you are trying to set is of type '{parameterSetter.SelectedParameter.Type}', but the value you set was of type 'String'");
					}

					break;
				default:
					views.ValueSelectionView.SetFeedbackMessage($"Unexpected parameter type");
					break;
			}

			//if (parameterSetter.SelectedParameter.Type == ParameterType.Double &&
			//	double.TryParse(parameterSetter.NewParameterValue, out double parsedDouble))
			//{
			//	SetParameterValue(engine, parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, (double?)parsedDouble);
			//}
			//else
			//{
			//	SetParameterValue(engine, parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, parameterSetter.NewParameterValue);
			//}

			//engine.ExitSuccess("Finish button was pressed.");
		}
	}
}
