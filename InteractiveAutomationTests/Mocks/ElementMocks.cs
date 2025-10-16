namespace InteractiveAutomationTests.Mocks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Moq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.ReportsAndDashboards;

	/// <summary>
	/// Mock of the elements.
	/// </summary>
	public class ElementMocks
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ElementMocks"/> class.
		/// Represents a DataMiner Automation script.
		/// </summary>
		/// <param name="protocolMocks">Mocks of the protocols.</param>
		public ElementMocks(ProtocolMocks protocolMocks)
		{
			var mocks = new MockRepository(MockBehavior.Default) { DefaultValue = DefaultValue.Mock };

			this.MicrosoftPlatformA = mocks.OneOf<IDmsElement>(
				element =>
					element.Name == "Microsoft Platform A" &&
					element.DmsElementId == new DmsElementId(581, 5) &&
					element.State == ElementState.Active);

			this.MicrosoftPlatformElementA = mocks.OneOf<Element>(
				element =>
					element.Name == "Microsoft Platform A" &&
					element.Protocol == protocolMocks.MicrosoftPlatform &&
					element.IsActive == true);

			this.MicrosoftPlatformB = mocks.OneOf<IDmsElement>(
				element =>
					element.Name == "Microsoft Platform B" &&
					element.DmsElementId == new DmsElementId(582, 6) &&
					element.State == ElementState.Active);

			this.MicrosoftPlatformC = mocks.OneOf<IDmsElement>(
				element =>
					element.Name == "Microsoft Platform C" &&
					element.DmsElementId == new DmsElementId(583, 7) &&
					element.State == ElementState.Active);

			this.Cbr8Main = mocks.OneOf<IDmsElement>(
				element =>
					element.Name == "CBR8 Main" &&
					element.DmsElementId == new DmsElementId(581, 12) &&
					element.State == ElementState.Active);

			this.Cbr8Backup = mocks.OneOf<IDmsElement>(
				element =>
					element.Name == "CBR8 Backup" &&
					element.DmsElementId == new DmsElementId(581, 13) &&
					element.State == ElementState.Active);

			this.All = new[] { this.MicrosoftPlatformA, this.MicrosoftPlatformB, this.MicrosoftPlatformC, this.Cbr8Main, this.Cbr8Backup };

			this.MicrosoftPlatformElements = new[] { this.MicrosoftPlatformA, this.MicrosoftPlatformB, this.MicrosoftPlatformC };
		}

		/// <summary>
		/// Gets the microsoft platform elements of the mock.
		/// </summary>
		public ICollection<IDmsElement> MicrosoftPlatformElements { get; }

		/// <summary>
		/// Gets the microsoft platform element A of the mock.
		/// </summary>
		public IDmsElement MicrosoftPlatformA { get; }

		/// <summary>
		/// Gets the microsoft platform element A of the mock.
		/// </summary>
		public Element MicrosoftPlatformElementA { get; }

		/// <summary>
		/// Gets the microsoft platform element B of the mock.
		/// </summary>
		public IDmsElement MicrosoftPlatformB { get; }

		/// <summary>
		/// Gets the microsoft platform element C of the mock.
		/// </summary>
		public IDmsElement MicrosoftPlatformC { get; }

		/// <summary>
		/// Gets the main converged broadband router element of the mock.
		/// </summary>
		public IDmsElement Cbr8Main { get; }

		/// <summary>
		/// Gets the backup converged broadband router element of the mock.
		/// </summary>
		public IDmsElement Cbr8Backup { get; }

		/// <summary>
		/// Gets all the elements of the mock.
		/// </summary>
		public ICollection<IDmsElement> All { get; }
	}
}