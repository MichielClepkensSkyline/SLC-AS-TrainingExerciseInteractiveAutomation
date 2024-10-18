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
				OnFinish(engine, parameterSetter);
		}

		private static void SetParameterValue<T>(IEngine engine, IDmsElement element, int parameterId, T value)
		{
			engine.GetDms()
				.GetAgent(element.AgentId)
				.GetElement(element.Name)
				.GetStandaloneParameter<T>(parameterId)
				.SetValue(value);
		}

		private void OnFinish(IEngine engine, ParameterSetter parameterSetter)
		{
			var dto = new ParameterSetterDto
			{
				SelectedElement = parameterSetter.SelectedElement,
				SelectedParameter = parameterSetter.SelectedParameter,
				NewParameterValue = parameterSetter.NewParameterValue,
			};

			engine.GenerateInformation(
				$"INFORMATION: {dto.SelectedElement.Name} / {dto.SelectedParameter.Name} / " +
				$"{dto.SelectedParameter.Type} / {dto.NewParameterValue}");

			if (dto.SelectedParameter.Type == ParameterType.Double &&
				double.TryParse(dto.NewParameterValue, out double parsedDouble))
			{
				SetParameterValue(engine, dto.SelectedElement, dto.SelectedParameter.Id, (double?)parsedDouble);
			}
			else
			{
				SetParameterValue(engine, dto.SelectedElement, dto.SelectedParameter.Id, dto.NewParameterValue);
			}

			engine.ExitSuccess("Finish button was pressed.");
		}
	}
}
