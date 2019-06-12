using System.Collections.Generic;

namespace Configuration.Cli
{
	public class Variable
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public Dictionary<string, string> Overrides { get; set; } = new Dictionary<string, string>();
	}
}

