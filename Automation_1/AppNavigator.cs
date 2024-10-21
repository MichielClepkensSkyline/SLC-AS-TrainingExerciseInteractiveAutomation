namespace Automation_1
{
	using System;
	using System.Globalization;
	using System.Text.RegularExpressions;

	using Automation_1.Dtos;
	using Automation_1.Enums;
	using Automation_1.Wizard.ElementSelection;
	using Automation_1.Wizard.ParameterSelection;
	using Automation_1.Wizard.ValueSelection;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Core.DataMinerSystem.Common.Selectors;
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

		private static void GenerateInputWidget(IEngine engine, ParameterSetter parameterSetter, ViewDto views)
		{
			var parameterType = GetParameterType(engine, parameterSetter);

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
					HandleNumericalParameterSet(engine, parameterSetter, views);
					break;
				default:
					views.ValueSelectionView.SetFeedbackMessage($"Unexpected parameter type");
					break;
			}
		}

		private static void HandleNumericalParameterSet(IEngine engine, ParameterSetter parameterSetter, ViewDto views)
		{
			var element = engine.FindElement(parameterSetter.SelectedElement.Name);
			var parameterDisplayValue = element.GetParameterDisplay(parameterSetter.SelectedParameter.Description);

			var newValue = parameterSetter.NewParameterValue;

			if (IsMatchingDateTimeFormat(parameterDisplayValue))
			{
				if (IsMatchingDateTimeFormat(newValue))
				{
					var parsedDateTime = DateTime.ParseExact(newValue, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
					var newDateTimeValue = parsedDateTime.ToOADate();

					SetParameterValue(engine, parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, (double?)newDateTimeValue);
					engine.ExitSuccess("The parameter value was set successfully.");
				}
				else
				{
					views.ValueSelectionView.SetFeedbackMessage("Please enter the DateTime in the format 'MM/DD/YYYY hh:mm:ss AM/PM'.");
				}
			}
			else
			{
				if (double.TryParse(newValue, out double parsedValue))
				{
					SetParameterValue(engine, parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, (double?)parsedValue);
					engine.ExitSuccess("The parameter value was set successfully.");
				}
				else
				{
					views.ValueSelectionView.SetFeedbackMessage("Invalid double value.");
				}
			}
		}

		private static ParameterType GetParameterType(IEngine engine, ParameterSetter parameterSetter)
		{
			var element = engine.FindElement(parameterSetter.SelectedElement.Name);
			var parameterDisplayValue = element.GetParameterDisplay(parameterSetter.SelectedParameter.Description);
			var parameterActualValue = element.GetParameter(parameterSetter.SelectedParameter.Description);

			if (IsMatchingDateTimeFormat(parameterDisplayValue))
			{
				return ParameterType.DateTime;
			}
			else if (double.TryParse(Convert.ToString(parameterActualValue), out double result))
			{
				return ParameterType.Double;
			}
			else
			{
				return ParameterType.String;
			}
		}

		private static bool IsMatchingDateTimeFormat(string input)
		{
			string pattern = @"^\d{1,2}/\d{1,2}/\d{4}\s\d{1,2}:\d{2}:\d{2}\s(?:AM|PM)$";

			return Regex.IsMatch(input, pattern);
		}
	}
}
