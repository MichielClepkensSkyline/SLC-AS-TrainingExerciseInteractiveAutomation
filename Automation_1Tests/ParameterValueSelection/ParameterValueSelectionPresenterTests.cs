namespace Automation_1.ParameterValueSelection.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Contains unit tests for the <see cref="ParameterValueSelectionPresenter"/> class.
	/// </summary>
	[TestClass]
	public class ParameterValueSelectionPresenterTests
	{
		/// <summary>
		/// Test that the presenter correctly sets string parameter.
		/// </summary>
		[TestMethod]
		public void OnSetStringValue_ValidInput_SetsParameter()
		{
			string parameterValue = "Value";
			int parameterId = 123;
			var stringTextBox = new TextBox { Text = parameterValue };
			var messageTextBox = new TextBox();
			var view = new Mock<IParameterValueSelectionView>();
			var parameter = new Mock<IDmsStandaloneParameter<string>>();
			var element = new Mock<IDmsElement>();
			var elementEngine = new Mock<Element>();
			var model = new Mock<IElementSelector>();
			var engine = new Mock<IEngine>();

			view.Setup(view => view.StringValue).Returns(stringTextBox);
			view.Setup(view => view.Message).Returns(messageTextBox);
			view.Setup(view => view.ExitButton).Returns(Mock.Of<Button>());
			view.Setup(view => view.BackButton).Returns(Mock.Of<Button>());
			view.Setup(view => view.SetStringValue).Returns(Mock.Of<Button>());
			view.Setup(view => view.SetDoubleValue).Returns(Mock.Of<Button>());
			parameter.Setup(parameter => parameter.SetValue(parameterValue));
			element.Setup(element => element.GetStandaloneParameter<string>(parameterId)).Returns(parameter.Object);
			element.Setup(element => element.State).Returns(Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active);
			element.Setup(element => element.AgentId).Returns(123);
			element.Setup(element => element.Id).Returns(456);
			elementEngine.Setup(elementEngine => elementEngine.IsActive).Returns(true);
			model.SetupProperty(model => model.SetParameterValueString);
			model.Setup(model => model.SelectedElement).Returns(element.Object);
			model.Setup(model => model.SelectedParameterId).Returns(parameterId);
			model.Setup(model => model.Parameters).Returns(new List<ParameterInfo>
				{
					new ParameterInfo { ID = parameterId, InterpreteType = ParameterInterpreteType.String },
				});
			engine.Setup(engine => engine.FindElement(123, 456)).Returns(elementEngine.Object);

			var presenter = new ParameterValueSelectionPresenter(engine.Object, view.Object, model.Object);

			presenter.OnSetStringValue();

			Assert.AreEqual(parameterValue, model.Object.SetParameterValueString);
			Assert.AreEqual("String parameter set successfull!", messageTextBox.Text);
			parameter.Verify(p => p.SetValue(parameterValue), Times.Once);
		}

		/// <summary>
		///  Test that the presenter correctly sets double parameter.
		/// </summary>
		[TestMethod]
		public void OnSetDoubleValue_ValidInput_SetsParameter()
		{
			double parameterValue = 10.2;
			int parameterId = 123;
			var stringTextBox = new Numeric { Value = parameterValue };
			var messageTextBox = new TextBox();
			var view = new Mock<IParameterValueSelectionView>();
			var parameter = new Mock<IDmsStandaloneParameter<double?>>();
			var element = new Mock<IDmsElement>();
			var elementEngine = new Mock<Element>();
			var model = new Mock<IElementSelector>();
			var engine = new Mock<IEngine>();

			view.Setup(view => view.DoubleValue).Returns(stringTextBox);
			view.Setup(view => view.Message).Returns(messageTextBox);
			view.Setup(view => view.ExitButton).Returns(Mock.Of<Button>());
			view.Setup(view => view.BackButton).Returns(Mock.Of<Button>());
			view.Setup(view => view.SetStringValue).Returns(Mock.Of<Button>());
			view.Setup(view => view.SetDoubleValue).Returns(Mock.Of<Button>());
			parameter.Setup(parameter => parameter.SetValue(parameterValue));
			element.Setup(element => element.GetStandaloneParameter<double?>(parameterId)).Returns(parameter.Object);
			element.Setup(element => element.State).Returns(Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active);
			element.Setup(element => element.AgentId).Returns(123);
			element.Setup(element => element.Id).Returns(456);
			elementEngine.Setup(elementEngine => elementEngine.IsActive).Returns(true);
			model.SetupProperty(model => model.SetParameterValueDouble);
			model.Setup(model => model.SelectedElement).Returns(element.Object);
			model.Setup(model => model.SelectedParameterId).Returns(parameterId);
			model.Setup(model => model.Parameters).Returns(new List<ParameterInfo>
				{
					new ParameterInfo { ID = parameterId, InterpreteType = ParameterInterpreteType.Double, RangeLow = 0, RangeHigh = 100 },
				});
			engine.Setup(engine => engine.FindElement(123, 456)).Returns(elementEngine.Object);

			var presenter = new ParameterValueSelectionPresenter(engine.Object, view.Object, model.Object);

			presenter.OnSetDoubleValue();

			Assert.AreEqual(parameterValue, model.Object.SetParameterValueDouble);
			Assert.AreEqual("Double parameter set successfull!", messageTextBox.Text);
			parameter.Verify(p => p.SetValue(parameterValue), Times.Once);
		}
	}
}
