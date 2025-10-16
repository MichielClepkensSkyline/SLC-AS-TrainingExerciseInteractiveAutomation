namespace InteractiveAutomationTests.Mocks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Moq;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;

	public class ParameterMocks
	{
		public ParameterMocks()
		{
			var mocks = new MockRepository(MockBehavior.Default) { DefaultValue = DefaultValue.Mock };

			this.Id = mocks.OneOf<ParameterInfo>(
				parameter =>
					parameter.Name == "Id" &&
					parameter.ID == 3 &&
					parameter.ParameterType == ParameterMeasurementType.Number &&
					parameter.WriteType == false);

			this.Name = mocks.OneOf<ParameterInfo>(
				parameter =>
					parameter.Name == "Name" &&
					parameter.ID == 21 &&
					parameter.ParameterType == ParameterMeasurementType.String &&
					parameter.WriteType == false);

			this.Value = mocks.OneOf<ParameterInfo>(
				parameter =>
					parameter.Name == "Value" &&
					parameter.ID == 22 &&
					parameter.ParameterType == ParameterMeasurementType.Analog &&
					parameter.WriteType == false);

			this.All = new[] { this.Id, this.Name, this.Value };
		}

		/// <summary>
		/// Gets the microsoft platform protocol of the mock.
		/// </summary>
		public ParameterInfo Id { get; }

		/// <summary>
		/// Gets the Cisco converged broadband router of the mock.
		/// </summary>
		public ParameterInfo Name { get; }

		/// <summary>
		/// Gets the generic ping protocol of the mock.
		/// </summary>
		public ParameterInfo Value { get; }

		/// <summary>
		/// Gets all the protocols of the mock.
		/// </summary>
		public IEnumerable<ParameterInfo> All { get; }
	}
}
