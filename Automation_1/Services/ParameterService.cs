namespace Automation_1.Services
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Text.RegularExpressions;

	using Automation_1.Enums;
	using Automation_1.Model;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ParameterService
	{
		private readonly IEngine _engine;

		public ParameterService(IEngine engine)
		{
			_engine = engine ?? throw new ArgumentNullException(nameof(engine));
		}

		public static ParameterType GetParameterType(IEngine engine, ParameterSetter parameterSetter)
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

		public void SetParameterValue<T>(IDmsElement element, int parameterId, T value)
		{
			_engine.GetDms()
				   .GetAgent(element.AgentId)
				   .GetElement(element.Name)
				   .GetStandaloneParameter<T>(parameterId)
				   .SetValue(value);
		}

		public bool TrySetNumericalParameter(ParameterSetter parameterSetter, string newValue, out string feedbackMessage)
		{
			feedbackMessage = string.Empty;
			var element = _engine.FindElement(parameterSetter.SelectedElement.Name);
			var parameterDisplayValue = element.GetParameterDisplay(parameterSetter.SelectedParameter.Description);

			if (IsMatchingDateTimeFormat(parameterDisplayValue))
			{
				if (IsMatchingDateTimeFormat(newValue))
				{
					string[] formats = { "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy HH:mm:ss", "dd/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy HH:mm:ss" };

					if (DateTime.TryParseExact(newValue, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
					{
						var newDateTimeValue = parsedDateTime.ToOADate();
						SetParameterValue(parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, (double?)newDateTimeValue);
						return true;
					}
					else
					{
						feedbackMessage = "Invalid DateTime format. Please enter in a supported format (e.g., 'MM/dd/yyyy hh:mm:ss AM/PM').";
						return false;
					}
				}
				else
				{
					feedbackMessage = "Invalid DateTime format. Please enter in a supported format.";
					return false;
				}
			}
			else if (double.TryParse(newValue, out double parsedValue))
			{
				SetParameterValue(parameterSetter.SelectedElement, parameterSetter.SelectedParameter.Id, (double?)parsedValue);
				return true;
			}
			else
			{
				feedbackMessage = "Invalid double value.";
				return false;
			}
		}

		private static bool IsMatchingDateTimeFormat(string input)
		{
			var datePatterns = new List<string>
			{
				@"^\d{1,2}/\d{1,2}/\d{4}\s\d{1,2}:\d{2}:\d{2}\s(?:AM|PM)$", // MM/dd/yyyy hh:mm:ss AM/PM
				@"^\d{1,2}/\d{1,2}/\d{4}\s\d{2}:\d{2}:\d{2}$",              // MM/dd/yyyy HH:mm:ss (24-hour)
				@"^\d{1,2}/\d{1,2}/\d{4}\s\d{1,2}:\d{2}:\d{2}\s(?:AM|PM)$", // dd/MM/yyyy hh:mm:ss AM/PM (European)
				@"^\d{1,2}/\d{1,2}/\d{4}\s\d{2}:\d{2}:\d{2}$",              // dd/MM/yyyy HH:mm:ss (European, 24-hour)
			};

			return datePatterns.Exists(pattern => Regex.IsMatch(input, pattern));
		}
	}
}
