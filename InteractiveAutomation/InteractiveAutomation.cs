/*
****************************************************************************
*  Copyright (c) 2025,  Skyline Communications NV  All Rights Reserved.    *
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

09/10/2025	1.0.0.1		SKF, Skyline	Initial version
****************************************************************************
*/
namespace InteractiveAutomation
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Reflection;
	using System.Runtime.Remoting.Channels;
	using System.Text;
	using InteractiveAutomation.Wizard.ElementSelection;
	using InteractiveAutomation.Wizard.ParameterSelection;
	using InteractiveAutomation.Wizard.SetValue;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		private InteractiveController app;
		private IEngine engine;

		/// <summary>
		/// The Script entry point.
		/// IEngine.ShowUI();.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			try
			{
				app = new InteractiveController(engine);

				engine.SetFlag(RunTimeFlags.NoKeyCaching);
				engine.Timeout = TimeSpan.FromHours(10);

				this.engine = engine;

				RunSafe(engine);
			}
			catch (ScriptAbortException)
			{
				throw;
			}
			catch (ScriptForceAbortException)
			{
				throw;
			}
			catch (ScriptTimeoutException)
			{
				throw;
			}
			catch (InteractiveUserDetachedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				engine.ExitFail($"Run|Something went wrong: {ex}");
			}
		}

		private void RunSafe(IEngine engine)
		{
			// TODO: Define dialogs here
			IModel model = new Model(engine.GetDms(), engine);
			ElementSelectionView elementSelectionDialog = new ElementSelectionView(engine);
			ElementSelectionPresenter elementSelectionPresenter = new ElementSelectionPresenter(elementSelectionDialog, model, engine);
			ParameterSelectionView parameterSelectionDialog = new ParameterSelectionView(engine);
			ParameterSelectionPresenter parameterSelectionPresenter = new ParameterSelectionPresenter(parameterSelectionDialog, model, engine);
			SetValueView setValueDialog = new SetValueView(engine);
			SetValuePresenter setValuePresenter = new SetValuePresenter(setValueDialog, model);

			// Define how windows move between each other
			elementSelectionPresenter.Next += (sender, args) =>
			{
				parameterSelectionPresenter.LoadFromModel();
				app.ShowDialog(parameterSelectionDialog);
			};

			parameterSelectionPresenter.Back += (sender, args) =>
			{
				elementSelectionPresenter.LoadFromModel();
				app.ShowDialog(elementSelectionDialog);
			};

			parameterSelectionPresenter.Next += (sender, args) =>
			{
				app.ShowDialog(setValueDialog);
			};

			setValuePresenter.Back += (sender, args) =>
			{
				app.ShowDialog(parameterSelectionDialog);
			};

			setValuePresenter.Finish += (sender, args) =>
			{
				engine.ExitSuccess("Script finished");
			};

			elementSelectionPresenter.LoadFromModel();

			app.ShowDialog(elementSelectionDialog);
		}
	}
}