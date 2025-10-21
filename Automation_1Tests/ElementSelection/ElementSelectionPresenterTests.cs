namespace Automation_1.ElementSelection.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Moq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Contains unit tests for the <see cref="ElementSelectionPresenter"/> class.
	/// </summary>
	[TestClass]
	public class ElementSelectionPresenterTests
	{
		/// <summary>
		/// Test that the presenter correctly populates the elements dropdown from the model when there is no elements.
		/// </summary>
		[TestMethod]
		public void LoadFromModel_NoElements_SetsEmptyDropdown()
		{
			Mock<IElementSelector> selector = new Mock<IElementSelector>();
			Mock<IElementSelectionView> view = new Mock<IElementSelectionView>();
			Mock<IEngine> engine = new Mock<IEngine>();
			DropDown dropDown = new DropDown();
			Button continueButton = new Button("Continue");
			view.Setup(v => v.ElementsDropDown).Returns(dropDown);
			view.Setup(v => v.ContinueButton).Returns(continueButton);
			selector.Setup(s => s.Elements).Returns(new List<IDmsElement>());

			var presenter = new ElementSelectionPresenter(engine.Object, view.Object, selector.Object);
			presenter.LoadFromModel();

			Assert.AreEqual(0, dropDown.Options.ToList().Count);
		}

		/// <summary>
		/// Test that the presenter correctly populates the elements dropdown from the model.
		/// </summary>
		[TestMethod]
		public void LoadFromModel_WithElements_PopulatesDropdownAndSelectsCorrectElement()
		{
			Mock<IElementSelector> selector = new Mock<IElementSelector>();
			Mock<IElementSelectionView> view = new Mock<IElementSelectionView>();
			Mock<IEngine> engine = new Mock<IEngine>();
			DropDown dropDown = new DropDown();
			Button continueButton = new Button("Continue");
			view.Setup(v => v.ElementsDropDown).Returns(dropDown);
			view.Setup(v => v.ContinueButton).Returns(continueButton);
			selector.Setup(s => s.Elements).Returns(new List<IDmsElement>());

			var element1 = new Mock<IDmsElement>();
			element1.Setup(e => e.Name).Returns("HTTP element");

			var element2 = new Mock<IDmsElement>();
			element2.Setup(e => e.Name).Returns("Starlink");

			var elements = new List<IDmsElement> { element1.Object, element2.Object };

			selector.Setup(s => s.Elements).Returns(elements);
			selector.Setup(s => s.SelectedElement).Returns(element1.Object);

			var presenter = new ElementSelectionPresenter(engine.Object,view.Object, selector.Object);
			presenter.LoadFromModel();

			CollectionAssert.AreEquivalent(new[] { "HTTP element", "Starlink" }, dropDown.Options.ToArray());
			Assert.AreEqual("HTTP element", dropDown.Selected);
		}
	}
}