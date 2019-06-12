using LibGit2Sharp;

namespace Configuration.Bootstrapper
{
	public class Bootstrapper
	{
		private const string REPOSITORY_URL = "https://github.com/vag1830/JJConfiguration.git";
		private const string REPOSITORY_PATH = "C:/mine/projects/ConfigurationPOC/ConfigurationRepo";
	}

	public static class BootstrapperExtensions
	{
		public static void InitialiseRepository(this Bootstrapper bootstrapper, RespositoryOptions options)
		{
			var probe = Repository.Discover(options.Path);
			var clone = Repository.Clone(options.Url, options.Path);
		}
	}

	public class RespositoryOptions
	{
		public string Url { get; set; }

		public string Path { get; set; }
	}
}
