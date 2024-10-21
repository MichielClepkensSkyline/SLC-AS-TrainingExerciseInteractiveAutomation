namespace Automation_1.Dtos
{
	using Automation_1.Enums;

	public class ParameterInfo
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public ParameterType Type { get; set; }

        public string Description { get; set; }
    }
}
