namespace InteractiveAutomation.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using FluentAssertions;
	using InteractiveAutomation;
	using InteractiveAutomationTests.Mocks;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	[TestClass]
	public class ModelTests
	{
		private MockRepository? mocks;
		private ParameterMocks? parameterMocks;
		private ProtocolMocks? protocolMocks;
		private ElementMocks? elementMocks;

		[TestInitialize]
		public void Setup()
		{
			this.mocks = new MockRepository(MockBehavior.Default) { DefaultValue = DefaultValue.Mock };
			this.parameterMocks = new ParameterMocks();
			this.protocolMocks = new ProtocolMocks(this.parameterMocks);
			this.elementMocks = new ElementMocks(this.protocolMocks);
		}

		[TestMethod()]
		public void NoNullsAfterConstructionTest()
		{
			// Arrange
			var dms = this.mocks.OneOf<IDms>(
				d =>
				d.GetElements() == this.elementMocks.All);
			var engine = this.mocks.OneOf<IEngine>(
				e =>
				e.FindElement(this.elementMocks.MicrosoftPlatformElementA.Name) == this.elementMocks.MicrosoftPlatformElementA);

			// Act
			Model model = new Model(dms, engine);

			// Assert
			model.Elements.Should().NotBeNull().And.BeEquivalentTo(this.elementMocks.All.ToDictionary(e => e.Name));
			model.SelectedElementName.Should().NotBeNull().And.Be(this.elementMocks.MicrosoftPlatformA.Name);
			model.Parameters.Should().NotBeNull().And.BeEquivalentTo(this.parameterMocks.All.ToDictionary(p => p.ID));
			model.SelectedParameterId.Should().Be(0); // Not pressed on next, so no ParameterId selected
		}

		[TestMethod()]
		public void SetStringOnParameterTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void SetDoubleOnParameterTest()
		{
			Assert.Fail();
		}
	}
}