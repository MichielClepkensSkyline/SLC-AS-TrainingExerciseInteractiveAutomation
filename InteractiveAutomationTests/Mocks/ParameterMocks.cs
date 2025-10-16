// <copyright file="ParameterMocks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace InteractiveAutomationTests.Mocks
{
	using System.Collections.Generic;
	using Moq;
	using Skyline.DataMiner.Net.Messages;

	/// <summary>
	/// Mock of the parameters.
	/// </summary>
	public class ParameterMocks
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ParameterMocks"/> class.
		/// Represents a DataMiner Automation script.
		/// </summary>
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

			this.StringValue = mocks.OneOf<ParameterInfo>(
				parameter =>
					parameter.Name == "Value" &&
					parameter.ID == 22 &&
					parameter.ParameterType == ParameterMeasurementType.String &&
					parameter.WriteType == false);

			this.DoubleValue = mocks.OneOf<ParameterInfo>(
				parameter =>
					parameter.Name == "Value" &&
					parameter.ID == 23 &&
					parameter.ParameterType == ParameterMeasurementType.Number &&
					parameter.WriteType == false);

			this.All = new[] { this.Id, this.Name, this.StringValue, this.DoubleValue };
		}

		/// <summary>
		/// Gets parameter info of the Id parameter.
		/// </summary>
		public ParameterInfo Id { get; }

		/// <summary>
		/// Gets parameter info of the Name parameter.
		/// </summary>
		public ParameterInfo Name { get; }

		/// <summary>
		/// Gets parameter info of the StringValue parameter.
		/// </summary>
		public ParameterInfo StringValue { get; }

		/// <summary>
		/// Gets parameter info of the DoubleValue parameter.
		/// </summary>
		public ParameterInfo DoubleValue { get; }

		/// <summary>
		/// Gets all parameter info.
		/// </summary>
		public IEnumerable<ParameterInfo> All { get; }
	}
}
