// <copyright file="ProtocolMocks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace InteractiveAutomationTests.Mocks
{
	using Moq;
	using Skyline.DataMiner.Net.Messages;

	/// <summary>
	/// Mock of the protocols.
	/// </summary>
	public class ProtocolMocks
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProtocolMocks"/> class.
		/// Represents a DataMiner Automation script.
		/// </summary>
		/// <param name="parameterMocks">Mocks of the parameters.</param>
		public ProtocolMocks(ParameterMocks parameterMocks)
		{
			var mocks = new MockRepository(MockBehavior.Default) { DefaultValue = DefaultValue.Mock };

			this.MicrosoftPlatform = mocks.OneOf<GetProtocolInfoResponseMessage>(
				protocol =>
					protocol.BaseParameters == parameterMocks.All);

			this.CiscoCbr8 = mocks.OneOf<GetProtocolInfoResponseMessage>(
				protocol =>
					protocol.BaseParameters == parameterMocks.All);

			this.GenericPing = mocks.OneOf<GetProtocolInfoResponseMessage>(
				protocol =>
					protocol.BaseParameters == parameterMocks.All);

			this.All = new[] { this.GenericPing, this.MicrosoftPlatform, this.CiscoCbr8 };
		}

		/// <summary>
		/// Gets the microsoft platform protocol of the mock.
		/// </summary>
		public GetProtocolInfoResponseMessage MicrosoftPlatform { get; }

		/// <summary>
		/// Gets the Cisco converged broadband router of the mock.
		/// </summary>
		public GetProtocolInfoResponseMessage CiscoCbr8 { get; }

		/// <summary>
		/// Gets the generic ping protocol of the mock.
		/// </summary>
		public GetProtocolInfoResponseMessage GenericPing { get; }

		/// <summary>
		/// Gets all the protocols of the mock.
		/// </summary>
		public GetProtocolInfoResponseMessage[] All { get; }
	}
}