namespace Automation_1.ElementSelection.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Automation_1.ElementSelection;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	[TestClass]
	public class ElementSelectionPresenterTests
	{
		[TestMethod]
		public void LoadFromModel_EmptyElements_SetsEmptyDropdown()
		{
			// Arrange
			var modelMock = new Mock<IElementSelector>();
			modelMock.Setup(m => m.Elements).Returns(new List<IDmsElement>());
			modelMock.Setup(m => m.SelectedElement).Returns((IDmsElement)null);

			var dropdownMock = new Mock<IDropDown>();
			var viewMock = new Mock<IElementSelectionView>();
			viewMock.Setup(v => v.ElementDropDown).Returns(dropdownMock.Object);
			viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());

			var presenter = new ElementSelectionPresenter(modelMock.Object, viewMock.Object);

			// Act + Assert
			Assert.ThrowsException<NullReferenceException>(() => presenter.LoadFromModel());
		}

		[TestMethod]
		public void LoadFromModel_ValidElements_PopulatesDropdownAndSetsSelected()
		{
			// Arrange
			var element1 = new Mock<IDmsElement>();
			element1.Setup(e => e.Name).Returns("ElementA");

			var element2 = new Mock<IDmsElement>();
			element2.Setup(e => e.Name).Returns("ElementB");

			var selectedElement = element2.Object;

			var modelMock = new Mock<IElementSelector>();
			modelMock.Setup(m => m.Elements).Returns(new List<IDmsElement> { element1.Object, element2.Object });
			modelMock.Setup(m => m.SelectedElement).Returns(selectedElement);

			var dropdownMock = new Mock<IDropDown>();

			var viewMock = new Mock<IElementSelectionView>();
			viewMock.Setup(v => v.ElementDropDown).Returns(dropdownMock.Object);
			viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());

			var presenter = new ElementSelectionPresenter(modelMock.Object, viewMock.Object);

			// Act
			presenter.LoadFromModel();

			// Assert
			dropdownMock.Verify(d => d.SetOptions(It.Is<IEnumerable<string>>(opts =>
				opts.Contains("ElementA") && opts.Contains("ElementB") && opts.Count() == 2)));

			dropdownMock.VerifySet(d => d.Selected = "ElementB");
		}

		[TestMethod]
		public void StoreToModel_SelectedElementIsActive_SetsModelAndFlag()
		{
			// Arrange
			var elementName = "ElementA";

			var dmsElementMock = new Mock<IDmsElement>();
			dmsElementMock.Setup(e => e.Name).Returns(elementName);
			dmsElementMock.Setup(e => e.State).Returns(ElementState.Active);

			var dropdownMock = new Mock<IDropDown>();
			dropdownMock.Setup(d => d.Selected).Returns(elementName);

			var viewMock = new Mock<IElementSelectionView>();
			viewMock.Setup(v => v.ElementDropDown).Returns(dropdownMock.Object);
			viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());

			var modelMock = new Mock<IElementSelector>();
			modelMock.SetupProperty(m => m.SelectedElement);

			var presenter = new ElementSelectionPresenter(modelMock.Object, viewMock.Object);
			presenter._elementsByName = new Dictionary<string, IDmsElement>
			{
				{ elementName, dmsElementMock.Object },
			};

			// Act
			presenter.StoreToModel();

			// Assert
			Assert.AreEqual(dmsElementMock.Object, modelMock.Object.SelectedElement);
			Assert.IsTrue(presenter.IsElementActive);
		}

		[TestMethod]
		public void StoreToModel_SelectedElementIsNotActive_SetsFlagFalse()
		{
			// Arrange
			var elementName = "ElementB";

			var dmsElementMock = new Mock<IDmsElement>();
			dmsElementMock.Setup(e => e.Name).Returns(elementName);
			dmsElementMock.Setup(e => e.State).Returns(ElementState.Paused);

			var dropdownMock = new Mock<IDropDown>();
			dropdownMock.Setup(d => d.Selected).Returns(elementName);

			var viewMock = new Mock<IElementSelectionView>();
			viewMock.Setup(v => v.ElementDropDown).Returns(dropdownMock.Object);
			viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());

			var modelMock = new Mock<IElementSelector>();
			modelMock.SetupProperty(m => m.SelectedElement);

			var presenter = new ElementSelectionPresenter(modelMock.Object, viewMock.Object);
			presenter._elementsByName = new Dictionary<string, IDmsElement>
			{
				{ elementName, dmsElementMock.Object },
			};

			// Act
			presenter.StoreToModel();

			// Assert
			Assert.AreEqual(dmsElementMock.Object, modelMock.Object.SelectedElement);
			Assert.IsFalse(presenter.IsElementActive);
		}

		[TestMethod]
		public void OnNextButtonPressed_StoresToModelAndRaisesNextEvent()
		{
			// Arrange
			var elementName = "ElementC";

			var dmsElementMock = new Mock<IDmsElement>();
			dmsElementMock.Setup(e => e.Name).Returns(elementName);
			dmsElementMock.Setup(e => e.State).Returns(ElementState.Active);

			var dropdownMock = new Mock<IDropDown>();
			dropdownMock.Setup(d => d.Selected).Returns(elementName);

			var viewMock = new Mock<IElementSelectionView>();
			viewMock.Setup(v => v.ElementDropDown).Returns(dropdownMock.Object);
			viewMock.Setup(v => v.NextButton).Returns(Mock.Of<Button>());

			var modelMock = new Mock<IElementSelector>();
			modelMock.SetupProperty(m => m.SelectedElement);

			var presenter = new ElementSelectionPresenter(modelMock.Object, viewMock.Object);
			presenter._elementsByName = new Dictionary<string, IDmsElement>
			{
				{ elementName, dmsElementMock.Object },
			};

			bool nextEventRaised = false;
			presenter.Next += (s, e) => nextEventRaised = true;

			// Act
			presenter.OnNextButtonPressed(null, EventArgs.Empty);

			// Assert
			Assert.AreEqual(dmsElementMock.Object, modelMock.Object.SelectedElement, "Selected element was not set correctly.");
			Assert.IsTrue(presenter.IsElementActive, "Element should be marked as active.");
			Assert.IsTrue(nextEventRaised, "Next event was not raised.");
		}
	}
}