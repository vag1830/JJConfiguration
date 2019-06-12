using System.Collections.Generic;

namespace Configuration.Cli
{
	public class ConfigurationModel
	{
		public string ApplicationName { get; set; }

		public string Version { get; set; }

		public List<string> Variables { get; set; } = new List<string>();
	}

	public class ApplicationConfiguration
	{
		public string ApplicationName { get; set; }

		public string Version { get; set; }

		public List<Variable> Variables { get; set; } = new List<Variable>();
	}
}