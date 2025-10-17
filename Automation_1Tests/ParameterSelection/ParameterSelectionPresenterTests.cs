using Automation_1.ParameterSelection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skyline.DataMiner.Core.DataMinerSystem.Common;
using Skyline.DataMiner.Net.Messages;
using Skyline.DataMiner.Utils.InteractiveAutomationScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_1.ParameterSelection.Tests
{
    [TestClass]
    public class ParameterSelectionPresenterTests
    {
        [TestMethod]
        public void OnNextButtonPressed_ParameterFound()
        {
            // Arrange
            string parameterName = "SomeParam";
            int parameterId = 1001;
            var parameterMock = new Mock<IDmsStandaloneParameter<string>>();
            parameterMock.Setup(p => p.GetValue()).Returns("someValue");

            var elementMock = new Mock<IDmsElement>();
            elementMock.Setup(e => e.GetStandaloneParameter<string>(It.IsAny<int>()))
                       .Returns(parameterMock.Object);

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.SelectedParameter, 1001);
            modelMock.Setup(m => m.SelectedElement).Returns(elementMock.Object);

            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(Mock.Of<IDropDown>());
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);
            presenter.parametersByName = new Dictionary<string, ParameterInfo>
            {
                { parameterName, new ParameterInfo { ID = parameterId } }
            };

            var dropdownMock = new Mock<IDropDown>();
            dropdownMock.Setup(d => d.Selected).Returns(parameterName);
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);

            // Act
            presenter.OnNextButtonPressed(null, EventArgs.Empty);

            // Assert
            Assert.IsTrue(presenter.ParameterExists);
        }

        [TestMethod]
        public void OnNextButtonPressed_ParameterThrows_SetsParameterExistsFalse()
        {
            // Arrange
            string parameterName = "SomeParam";
            int parameterId = 1001;
            var elementMock = new Mock<IDmsElement>();
            elementMock.Setup(e => e.GetStandaloneParameter<string>(It.IsAny<int>()))
                       .Throws(new Exception("Parameter not found"));

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.SelectedParameter, parameterId);
            modelMock.Setup(m => m.SelectedElement).Returns(elementMock.Object);

            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(Mock.Of<IDropDown>());
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);
            presenter.parametersByName = new Dictionary<string, ParameterInfo>
            {
                { parameterName, new ParameterInfo { ID = parameterId } }
            };

            var dropdownMock = new Mock<IDropDown>();
            dropdownMock.Setup(d => d.Selected).Returns(parameterName);
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);

            // Act
            presenter.OnNextButtonPressed(null, EventArgs.Empty);

            // Assert
            Assert.IsFalse(presenter.ParameterExists);
        }

        [TestMethod]
        public void OnNextButtonPressed_RaisesNextEvent()
        {
            // Arrange
            string parameterName = "SomeParam";
            int parameterId = 1001;
            var parameterMock = new Mock<IDmsStandaloneParameter<string>>();
            parameterMock.Setup(p => p.GetValue()).Returns("value");

            var elementMock = new Mock<IDmsElement>();
            elementMock.Setup(e => e.GetStandaloneParameter<string>(It.IsAny<int>()))
                       .Returns(parameterMock.Object);

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.SelectedParameter, parameterId);
            modelMock.Setup(m => m.SelectedElement).Returns(elementMock.Object);

            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(Mock.Of<IDropDown>());
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);
            presenter.parametersByName = new Dictionary<string, ParameterInfo>
            {
                { parameterName, new ParameterInfo { ID = parameterId } }
            };

            var dropdownMock = new Mock<IDropDown>();
            dropdownMock.Setup(d => d.Selected).Returns("SomeParam");
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);

            bool eventRaised = false;
            presenter.Next += (s, e) => eventRaised = true;

            // Act
            presenter.OnNextButtonPressed(null, EventArgs.Empty);

            // Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void OnBackButtonPressed_CallsStoreToModelAndRaisesBackEvent()
        {
            // Arrange
            string parameterName = "BackParam";
            int parameterId = 42;
            var dropdownMock = new Mock<IDropDown>();
            dropdownMock.Setup(d => d.Selected).Returns(parameterName);

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.SelectedParameter);

            var backButtonMock = new Mock<Button>();
            var nextButtonMock = new Mock<Button>();

            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);
            viewMock.Setup(v => v.BackButton).Returns(backButtonMock.Object);
            viewMock.Setup(v => v.NextButton).Returns(nextButtonMock.Object);

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);
            presenter.parametersByName = new Dictionary<string, ParameterInfo>
            {
                { parameterName, new ParameterInfo { ID = parameterId } }
            };

            bool backEventRaised = false;
            presenter.Back += (s, e) => backEventRaised = true;

            // Act
            presenter.OnBackButtonPressed(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual(parameterId, modelMock.Object.SelectedParameter, "StoreToModel did not set the correct parameter.");
            Assert.IsTrue(backEventRaised, "Back event was not raised.");
        }


        [TestMethod]
        public void StoreToModel_ValidSelection_SetsModelParameterId()
        {
            // Arrange
            var dropdownMock = new Mock<IDropDown>();
            dropdownMock.Setup(d => d.Selected).Returns("ParamA");

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.SelectedParameter);

            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);

            string parameterName = "ParamA";
            int parameterIndex = 101;
            presenter.parametersByName = new Dictionary<string, ParameterInfo>
            {
                { parameterName, new ParameterInfo { ID = parameterIndex } }
            };

            // Act
            presenter.StoreToModel();

            // Assert
            Assert.AreEqual(parameterIndex, modelMock.Object.SelectedParameter);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void StoreToModel_InvalidSelection_ThrowsKeyNotFoundException()
        {
            // Arrange
            var dropdownMock = new Mock<IDropDown>();
            dropdownMock.Setup(d => d.Selected).Returns("MissingParam");

            var modelMock = new Mock<IElementSelector>();
            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);
            presenter.parametersByName = new Dictionary<string, ParameterInfo>(); 

            // Act
            presenter.StoreToModel();
        }

        [TestMethod]
        public void StoreToModel_ChoosesCorrectParameterFromMultiple()
        {
            // Arrange
            var dropdownMock = new Mock<IDropDown>();
            dropdownMock.Setup(d => d.Selected).Returns("ParamB");

            var modelMock = new Mock<IElementSelector>();
            modelMock.SetupProperty(m => m.SelectedParameter);

            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);
            presenter.parametersByName = new Dictionary<string, ParameterInfo>
            {
                { "ParamA", new ParameterInfo { ID = 1 } },
                { "ParamB", new ParameterInfo { ID = 2 } },
                { "ParamC", new ParameterInfo { ID = 3 } }
            };
            int expectedparameter = 2;

            // Act
            presenter.StoreToModel();

            // Assert
            Assert.AreEqual(expectedparameter, modelMock.Object.SelectedParameter);
        }

        [TestMethod]
        public void LoadFromModel_ElementsNull_SetsEmptyOptions()
        {
            var dropdownMock = new Mock<IDropDown>();
            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var modelMock = new Mock<IElementSelector>();
            modelMock.Setup(m => m.Elements).Returns((IReadOnlyCollection<IDmsElement>)null);

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);

            // Act
            presenter.LoadFromModel();

            // Assert
            dropdownMock.Verify(d => d.SetOptions(It.Is<IEnumerable<string>>(l => !l.Any())));
        }

        [TestMethod]
        public void LoadFromModel_ParametersNull_SetsEmptyOptions()
        {
            var dropdownMock = new Mock<IDropDown>();
            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var modelMock = new Mock<IElementSelector>();
            modelMock.Setup(m => m.Elements).Returns(new List<IDmsElement> { Mock.Of<IDmsElement>() });
            modelMock.Setup(m => m.Parameters).Returns((IEnumerable<ParameterInfo>)null);

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);

            // Act
            presenter.LoadFromModel();

            // Assert
            dropdownMock.Verify(d => d.SetOptions(It.Is<IEnumerable<string>>(l => !l.Any())));
        }

        [TestMethod]
        public void LoadFromModel_ValidData_SetsDropdownOptionsAndSelected()
        {
            var dropdownMock = new Mock<IDropDown>();

            var viewMock = new Mock<IParameterSelectionView>();
            viewMock.Setup(v => v.ParameterIdDropDown).Returns(dropdownMock.Object);
            viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());
            viewMock.Setup(v => v.BackButton).Returns(Mock.Of<Button>());

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo { ID = 1 },
                new ParameterInfo { ID = 2 },
                new ParameterInfo { ID = 3 }
            };

            var modelMock = new Mock<IElementSelector>();
            modelMock.Setup(m => m.Elements).Returns(new List<IDmsElement> { Mock.Of<IDmsElement>() });
            modelMock.Setup(m => m.Parameters).Returns(parameters);
            modelMock.Setup(m => m.SelectedParameter).Returns(2);

            var presenter = new ParameterSelectionPresenter(modelMock.Object, viewMock.Object);

            // Act
            presenter.LoadFromModel();

            // Assert
            dropdownMock.Verify(d => d.SetOptions(It.Is<IEnumerable<string>>(opts =>
                opts.Contains("1") && opts.Contains("2") && opts.Contains("3")
            )));

            dropdownMock.VerifySet(d => d.Selected = "2");
            Assert.AreEqual(3, presenter.parametersByName.Count);
        }

    }
}