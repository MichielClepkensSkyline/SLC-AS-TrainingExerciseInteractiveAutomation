namespace Automation_1.Dtos
{
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ParameterSetterDto
	{
		public IDmsElement SelectedElement { get; set; }

		public ParameterInfo SelectedParameter { get; set; }

		public string NewParameterValue { get; set; }
	}
}
