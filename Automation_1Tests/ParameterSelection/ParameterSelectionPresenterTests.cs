namespace Automation_1.ParameterSelection.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Contains unit tests for the <see cref="ParameterSelectionPresenter"/> class.
	/// </summary>
	[TestClass]
	public class ParameterSelectionPresenterTests
	{
		/// <summary>
		/// Test that the presenter correctly populates the parameters dropdown from the model with no parameters.
		/// </summary>
		[TestMethod]
		public void LoadFromModel_NoParameters_SetsEmptyDropdown()
		{
			Mock<IElementSelector> selector = new Mock<IElementSelector>();
			Mock<IParameterSelectionView> view = new Mock<IParameterSelectionView>();
			Mock<IEngine> engine = new Mock<IEngine>();
			DropDown dropDown = new DropDown();
			Button continueButton = new Button("Continue");
			Button backButton = new Button("Back");

			view.Setup(v => v.ParameterIdDropDown).Returns(dropDown);
			view.Setup(v => v.ContinueButton).Returns(continueButton);
			view.Setup(v => v.BackButton).Returns(backButton);
			selector.Setup(s => s.Elements).Returns(new List<IDmsElement> { Mock.Of<IDmsElement>() });
			selector.Setup(s => s.Parameters).Returns(new List<ParameterInfo>());

			ParameterSelectionPresenter presenter = new ParameterSelectionPresenter(engine.Object, view.Object, selector.Object);

			presenter.LoadFromModel();

			Assert.AreEqual(0, dropDown.Options.ToList().Count);
		}

		/// <summary>
		/// Test that the presenter correctly populates the parameters dropdown from the model.
		/// </summary>
		[TestMethod]
		public void LoadFromModel_WithParameters_SetsDropdownOptions()
		{
			Mock<IElementSelector> selector = new Mock<IElementSelector>();
			Mock<IParameterSelectionView> view = new Mock<IParameterSelectionView>();
			Mock<IEngine> engine = new Mock<IEngine>();
			DropDown dropDown = new DropDown();
			Button continueButton = new Button("Continue");
			Button backButton = new Button("Back");
			var parameter = new ParameterInfo { ID = 312 };
			selector.Setup(s => s.Elements).Returns(new List<IDmsElement> { Mock.Of<IDmsElement>() });
			selector.Setup(s => s.Parameters).Returns(new List<ParameterInfo> { parameter });
			selector.Setup(s => s.SelectedParameterId).Returns(312);
			view.Setup(v => v.ParameterIdDropDown).Returns(dropDown);
			view.Setup(v => v.ContinueButton).Returns(continueButton);
			view.Setup(v => v.BackButton).Returns(backButton);
			ParameterSelectionPresenter presenter = new ParameterSelectionPresenter(engine.Object, view.Object, selector.Object);

			presenter.LoadFromModel();

			Assert.IsTrue(dropDown.Options.Contains("312"));
			Assert.AreEqual("312", dropDown.Selected);
		}
	}
}