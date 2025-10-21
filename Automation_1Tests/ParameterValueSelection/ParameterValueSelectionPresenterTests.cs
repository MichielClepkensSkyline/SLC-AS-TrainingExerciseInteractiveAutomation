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
			view.Setup(v => v.StringValue).Returns(stringTextBox);
			view.Setup(v => v.Message).Returns(messageTextBox);
			view.Setup(v => v.ExitButton).Returns(Mock.Of<Button>());
			view.Setup(v => v.BackButton).Returns(Mock.Of<Button>());
			view.Setup(v => v.SetStringValue).Returns(Mock.Of<Button>());
			view.Setup(v => v.SetDoubleValue).Returns(Mock.Of<Button>());

			var parameter = new Mock<IDmsStandaloneParameter<string>>();
			parameter.Setup(p => p.SetValue(parameterValue));

			var element = new Mock<IDmsElement>();
			element.Setup(e => e.GetStandaloneParameter<string>(parameterId)).Returns(parameter.Object);
			element.Setup(e => e.State).Returns(Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active);
			element.Setup(e => e.AgentId).Returns(123);
			element.Setup(e => e.Id).Returns(456);

			var elementEngine = new Mock<Element>();
			elementEngine.Setup(e => e.IsActive).Returns(true);

			var model = new Mock<IElementSelector>();
			model.SetupProperty(m => m.SetParameterValueString);
			model.Setup(m => m.SelectedElement).Returns(element.Object);
			model.Setup(m => m.SelectedParameterId).Returns(parameterId);
			model.Setup(m => m.Parameters).Returns(new List<ParameterInfo>
				{
					new ParameterInfo { ID = parameterId, InterpreteType = ParameterInterpreteType.String },
				});

			var engineMock = new Mock<IEngine>();
			engineMock.Setup(e => e.FindElement(123, 456)).Returns(elementEngine.Object);

			var presenter = new ParameterValueSelectionPresenter(engineMock.Object, view.Object, model.Object);

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
			view.Setup(v => v.DoubleValue).Returns(stringTextBox);
			view.Setup(v => v.Message).Returns(messageTextBox);
			view.Setup(v => v.ExitButton).Returns(Mock.Of<Button>());
			view.Setup(v => v.BackButton).Returns(Mock.Of<Button>());
			view.Setup(v => v.SetStringValue).Returns(Mock.Of<Button>());
			view.Setup(v => v.SetDoubleValue).Returns(Mock.Of<Button>());

			var parameter = new Mock<IDmsStandaloneParameter<double?>>();
			parameter.Setup(p => p.SetValue(parameterValue));

			var element = new Mock<IDmsElement>();
			element.Setup(e => e.GetStandaloneParameter<double?>(parameterId)).Returns(parameter.Object);
			element.Setup(e => e.State).Returns(Skyline.DataMiner.Core.DataMinerSystem.Common.ElementState.Active);
			element.Setup(e => e.AgentId).Returns(123);
			element.Setup(e => e.Id).Returns(456);

			var elementEngine = new Mock<Element>();
			elementEngine.Setup(e => e.IsActive).Returns(true);

			var model = new Mock<IElementSelector>();
			model.SetupProperty(m => m.SetParameterValueDouble);
			model.Setup(m => m.SelectedElement).Returns(element.Object);
			model.Setup(m => m.SelectedParameterId).Returns(parameterId);
			model.Setup(m => m.Parameters).Returns(new List<ParameterInfo>
				{
					new ParameterInfo { ID = parameterId, InterpreteType = ParameterInterpreteType.Double, RangeLow = 0, RangeHigh = 100 },
				});

			var engineMock = new Mock<IEngine>();
			engineMock.Setup(e => e.FindElement(123, 456)).Returns(elementEngine.Object);

			var presenter = new ParameterValueSelectionPresenter(engineMock.Object, view.Object, model.Object);

			presenter.OnSetDoubleValue();

			Assert.AreEqual(parameterValue, model.Object.SetParameterValueDouble);
			Assert.AreEqual("Double parameter set successfull!", messageTextBox.Text);
			parameter.Verify(p => p.SetValue(parameterValue), Times.Once);
		}
	}
}
