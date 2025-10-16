// <copyright file="ModelTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace InteractiveAutomation.Tests
{
	using System.Linq;
	using FluentAssertions;
	using InteractiveAutomation;
	using InteractiveAutomationTests.Mocks;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	/// <summary>
	/// Testclass for the model.
	/// </summary>
	[TestClass]
	public class ModelTests
	{
		private MockRepository? mocks;
		private ParameterMocks? parameterMocks;
		private ProtocolMocks? protocolMocks;
		private ElementMocks? elementMocks;

		/// <summary>
		/// Before running the test, this method is run.
		/// </summary>
		[TestInitialize]
		public void Setup()
		{
			this.mocks = new MockRepository(MockBehavior.Default) { DefaultValue = DefaultValue.Mock };
			this.parameterMocks = new ParameterMocks();
			this.protocolMocks = new ProtocolMocks(this.parameterMocks);
			this.elementMocks = new ElementMocks(this.protocolMocks, this.parameterMocks);
		}

		/// <summary>
		/// Test on the initialization of the model class.
		/// </summary>
		[TestMethod]
		public void NoNullsAfterConstructionTest()
		{
			// Arrange
			var dms = this.mocks?.OneOf<IDms>(
				d =>
				d.GetElements() == this.elementMocks.All);
			var engine = this.mocks?.OneOf<IEngine>(
				e =>
				e.FindElement(this.elementMocks.MicrosoftPlatformElementA.Name) == this.elementMocks.MicrosoftPlatformElementA);

			// Act
			IModel model = new Model(dms, engine);

			// Assert
			model.Elements.Should().NotBeNull().And.BeEquivalentTo(this.elementMocks?.All.ToDictionary(e => e.Name));
			model.SelectedElementName.Should().NotBeNull().And.Be(this.elementMocks?.MicrosoftPlatformA.Name);
			model.Parameters.Should().NotBeNull().And.BeEquivalentTo(this.parameterMocks?.All.ToDictionary(p => p.ID));
			model.SelectedParameterId.Should().Be(0);
		}
	}
}