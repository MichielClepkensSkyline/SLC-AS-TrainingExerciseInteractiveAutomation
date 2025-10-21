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

			view.Setup(view => view.ParameterIdDropDown).Returns(dropDown);
			view.Setup(view => view.ContinueButton).Returns(continueButton);
			view.Setup(view => view.BackButton).Returns(backButton);
			selector.Setup(selector => selector.Elements).Returns(new List<IDmsElement> { Mock.Of<IDmsElement>() });
			selector.Setup(selector => selector.Parameters).Returns(new List<ParameterInfo>());

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
			var parameter = new ParameterInfo { ID = 312, Name = "Audio Output Level" };

			selector.Setup(selector => selector.Elements).Returns(new List<IDmsElement> { Mock.Of<IDmsElement>() });
			selector.Setup(selector => selector.Parameters).Returns(new List<ParameterInfo> { parameter });
			selector.Setup(selector => selector.SelectedParameterId).Returns(312);
			view.Setup(view => view.ParameterIdDropDown).Returns(dropDown);
			view.Setup(view => view.ContinueButton).Returns(continueButton);
			view.Setup(view => view.BackButton).Returns(backButton);

			ParameterSelectionPresenter presenter = new ParameterSelectionPresenter(engine.Object, view.Object, selector.Object);

			presenter.LoadFromModel();

			Assert.IsTrue(dropDown.Options.Contains("Audio Output Level (312)"));
			Assert.AreEqual("312", dropDown.Selected);
		}
	}
}