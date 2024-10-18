/*
****************************************************************************
*  Copyright (c) 2024,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

22/01/2024	1.0.0.1		XXX, Skyline	Initial version
****************************************************************************
*/

namespace Automation_1
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	using System.Threading;

	using Automation_1.Enums;
	using Automation_1.Wizard.ElementSelection;
	using Automation_1.Wizard.ParameterSelection;
	using Automation_1.Wizard.ValueSelection;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Net.Messages;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// engine.ShowUI();
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			var controller = new InteractiveController(engine);

			var parameterSetter = new ParameterSetter(engine);

			var elementSelectionView = new ElementSelectionView(engine);
			var elementSelectionPresenter = new ElementSelectionPresenter(elementSelectionView, parameterSetter);

			var parameterSelectionView = new ParameterSelectionView(engine);
			var parameterSelectionPresenter = new ParameterSelectionPresenter(parameterSelectionView, parameterSetter);

			var valueSelectionView = new ValueSelectionView(engine);
			var valueSelectionPresenter = new ValueSelectionPresenter(valueSelectionView, parameterSetter);

			elementSelectionPresenter.LoadFromModel();

			elementSelectionPresenter.Continue += (sender, args) =>
			{
				parameterSelectionPresenter.LoadFromModel();
				controller.ShowDialog(parameterSelectionView);
			};

			parameterSelectionPresenter.Back += (sender, args) => controller.ShowDialog(elementSelectionView);

			parameterSelectionPresenter.Continue += (sender, args) => controller.ShowDialog(valueSelectionView);

			valueSelectionPresenter.Back += (sender, args) => controller.ShowDialog(parameterSelectionView);

			valueSelectionPresenter.Exit += (sender, args) => controller.Stop();

			valueSelectionPresenter.Finish += (sender, args) =>
			{
				var element = parameterSetter.SelectedElement;
				var parameter = parameterSetter.SelectedParameter;
				var value = parameterSetter.NewParameterValue;

				engine.GenerateInformation($"INFORMACIJEEEE: {element.Name} / {parameter.Name} / {parameter.Type} / {value}");

				if (parameter.Type == ParameterType.Double && double.TryParse(value, out double parsedDouble))
				{
					engine.GetDms().GetAgent(element.AgentId).GetElement(element.Name).GetStandaloneParameter<double?>(parameter.Id).SetValue(parsedDouble);
				}
				else
				{
					engine.GetDms().GetAgent(element.AgentId).GetElement(element.Name).GetStandaloneParameter<string>(parameter.Id).SetValue(value);
				}

				engine.ExitSuccess("Finish button was pressed.");
			};

			controller.ShowDialog(elementSelectionView);
		}
	}
}