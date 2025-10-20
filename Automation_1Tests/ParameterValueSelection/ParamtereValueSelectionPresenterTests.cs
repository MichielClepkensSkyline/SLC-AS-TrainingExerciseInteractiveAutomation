using Automation_1.ParameterValueSelection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Core.DataMinerSystem.Common;
using Skyline.DataMiner.Net.Messages;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterValueSelection.Tests
{
    [TestClass]
    public class ParamtereValueSelectionPresenterTests
    {
        [TestMethod]
        public void OnSetStringValueButtonPressed_ValidInput_SetsParameterAndShowsSuccess()
        {
            // Arrange
            string testValue = "Test123";
            int testParamId = 123;

            var stringTextBox = new TextBox { Text = testValue };
            var exceptionTextBox = new TextBox();

            var viewMock = new Mock<IParameterValueSelectionView>();
            viewMock.Setup(v => v.StringValueTextBox).Returns(stringTextBox);
            viewMock.Setup(v => v.ExceptionTextBox).Returns(exceptionTextBox);
            viewMock.Setup(v => v.ExitButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetStringVauleButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetDoubleValueButton).Returns(Mock.Of<Button>());

            var parameterMock = new Mock<IDmsStandaloneParameter<string>>();
            parameterMock.Setup(p => p.SetValue(testValue));

            var elementMock = new Mock<IDmsElement>();
            elementMock.Setup(e => e.GetStandaloneParameter<string>(testParamId)).Returns(parameterMock.Object);

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.StringValue);
            modelMock.Setup(m => m.SelectedElement).Returns(elementMock.Object);
            modelMock.Setup(m => m.SelectedParameter).Returns(testParamId);
            modelMock.Setup(m => m.Parameters).Returns(new List<ParameterInfo>
            {
                new ParameterInfo { ID = testParamId, InterpreteType = ParameterInterpreteType.String }
            });

            var engineMock = new Mock<IEngine>();

            var presenter = new ParamtereValueSelectionPresenter(modelMock.Object, viewMock.Object, engineMock.Object);

            // Act
            presenter.OnSetStringValueButtonPressed(null, EventArgs.Empty);

            // Assert
            parameterMock.Verify(p => p.SetValue(testValue), Times.Once);
            Assert.AreEqual(testValue, modelMock.Object.StringValue);
            Assert.AreEqual("Success", exceptionTextBox.Text);

        }

        [TestMethod]
        public void OnSetStringValueButtonPressed_EmptyInput_ShowsErrorAndDoesNotSetValue()
        {
            // Arrange
            string testValue = "   ";  // whitespace
            var stringTextBox = new TextBox { Text = testValue };
            var exceptionTextBox = new TextBox();

            var viewMock = new Mock<IParameterValueSelectionView>();
            viewMock.Setup(v => v.StringValueTextBox).Returns(stringTextBox);
            viewMock.Setup(v => v.ExceptionTextBox).Returns(exceptionTextBox);
            viewMock.Setup(v => v.ExitButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetStringVauleButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetDoubleValueButton).Returns(Mock.Of<Button>());

            var modelMock = new Mock<IElementSelector>();
            // No need to set parameters here because method returns early

            var engineMock = new Mock<IEngine>();

            var presenter = new ParamtereValueSelectionPresenter(modelMock.Object, viewMock.Object, engineMock.Object);

            // Act
            presenter.OnSetStringValueButtonPressed(null, EventArgs.Empty);

            // Assert
            // Verify SetValue never called by absence of parameter mock, so just check error text
            Assert.AreEqual("Please enter a valid string value", exceptionTextBox.Text);
        }

        [TestMethod]
        public void OnSetStringValueButtonPressed_NonStringParameter_ShowsErrorAndDoesNotSetValue()
        {
            // Arrange
            string testValue = "Test123";
            int testParamId = 123;

            var stringTextBox = new TextBox { Text = testValue };
            var exceptionTextBox = new TextBox();

            var viewMock = new Mock<IParameterValueSelectionView>();
            viewMock.Setup(v => v.StringValueTextBox).Returns(stringTextBox);
            viewMock.Setup(v => v.ExceptionTextBox).Returns(exceptionTextBox);
            viewMock.Setup(v => v.ExitButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetStringVauleButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetDoubleValueButton).Returns(Mock.Of<Button>());

            var elementMock = new Mock<IDmsElement>();

            var modelMock = new Mock<IElementSelector>();
            modelMock.Setup(m => m.SelectedElement).Returns(elementMock.Object);
            modelMock.Setup(m => m.SelectedParameter).Returns(testParamId);
            modelMock.Setup(m => m.Parameters).Returns(new List<ParameterInfo>
            {
                new ParameterInfo { ID = testParamId, InterpreteType = ParameterInterpreteType.Double } // Not string
            });

            var engineMock = new Mock<IEngine>();

            var presenter = new ParamtereValueSelectionPresenter(modelMock.Object, viewMock.Object, engineMock.Object);

            // Act
            presenter.OnSetStringValueButtonPressed(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual("Parameter isn't of type strng", exceptionTextBox.Text);
        }

        [TestMethod]
        public void OnSetDoubleValueButtonPressed_ValidValue_SetsParameterAndShowsSuccess()
        {
            // Arrange
            double testValue = 42.5;
            int testParamId = 123;

            var viewMock = new Mock<IParameterValueSelectionView>();
            viewMock.Setup(v => v.DoubleValueNumeric).Returns(new Numeric { Value = testValue });
            viewMock.Setup(v => v.ExceptionTextBox).Returns(new TextBox());
            viewMock.Setup(v => v.ExitButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetStringVauleButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetDoubleValueButton).Returns(Mock.Of<Button>());

            var parameterMock = new Mock<IDmsStandaloneParameter<double?>>();
            parameterMock.Setup(p => p.SetValue(testValue));

            var elementMock = new Mock<IDmsElement>();
            elementMock.Setup(e => e.GetStandaloneParameter<double?>(testParamId)).Returns(parameterMock.Object);

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.DoubleValue);
            modelMock.Setup(m => m.SelectedElement).Returns(elementMock.Object);
            modelMock.Setup(m => m.SelectedParameter).Returns(testParamId);
            modelMock.Setup(m => m.Parameters).Returns(new List<ParameterInfo>
            {
                new ParameterInfo { ID = testParamId, InterpreteType = ParameterInterpreteType.Double }
            });

            var engineMock = new Mock<IEngine>();

            var presenter = new ParamtereValueSelectionPresenter(modelMock.Object, viewMock.Object, engineMock.Object);

            // Act
            presenter.OnSetDoubleValueButtonPressed(null, EventArgs.Empty);

            // Assert
            parameterMock.Verify(p => p.SetValue(testValue), Times.Once);
            Assert.AreEqual(testValue, modelMock.Object.DoubleValue);
            Assert.AreEqual("Success", viewMock.Object.ExceptionTextBox.Text);
        }

        [TestMethod]
        public void OnSetDoubleValueButtonPressed_NegativeValue_ShowsErrorAndDoesNotSetValue()
        {
            // Arrange
            double testValue = -5.0;

            var exceptionTextBox = new TextBox();

            var viewMock = new Mock<IParameterValueSelectionView>();
            viewMock.Setup(v => v.DoubleValueNumeric).Returns(new Numeric { Value = testValue });
            viewMock.Setup(v => v.ExceptionTextBox).Returns(exceptionTextBox);
            viewMock.Setup(v => v.ExitButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetStringVauleButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.SetDoubleValueButton).Returns(Mock.Of<Button>());

            var modelMock = new Mock<IElementSelector>();

            var engineMock = new Mock<IEngine>();

            var presenter = new ParamtereValueSelectionPresenter(modelMock.Object, viewMock.Object, engineMock.Object);

            // Act
            presenter.OnSetDoubleValueButtonPressed(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual("Please enter a valid double value", exceptionTextBox.Text);
        }

    }
}