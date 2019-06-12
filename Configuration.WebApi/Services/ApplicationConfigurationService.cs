using System.Collections.Generic;
using System.IO;
using Configuration.Cli;
using Configuration.WebApi.Providers;
using Newtonsoft.Json;

namespace Configuration.WebApi.Services
{
	public interface IApplicationConfigurationService
	{
		void Add(string applicationName);

		ConfigurationModel Get(string applicationName);

		ConfigurationModel GetPublished(string applicationName, string version);

		void AddVariable(string applicationName, string variableName);

		void AddVariables(string applicationName, IEnumerable<string> variableNames);

		void Publish(string applicationName, string version);

		void Build(string applicationName, string version);
	}

	public class ApplicationConfigurationService : IApplicationConfigurationService
	{
		private const string CONFIGURATIONS_PATH = "configurations";

		private readonly IGitRepositoryProvider _gitRepositoryProvider = new GitRepositoryProvider();

		/// <inheritdoc />
		public void Add(string applicationName)
		{
			var repositoryPath = Config.RepositoryPath;

			var directory = Path.Combine(repositoryPath, CONFIGURATIONS_PATH, applicationName);

			Directory.CreateDirectory(directory);

			var fileName = $"{applicationName}.json";
			
			var absolutePath = Path.Combine(directory, fileName);

			using (var file = File.CreateText(absolutePath))
			{
				var serializer = new JsonSerializer();

				var newConfig = new ConfigurationModel
				{
					ApplicationName = applicationName
				};

				serializer.Serialize(file, newConfig);
			}

			_gitRepositoryProvider.Add($"{CONFIGURATIONS_PATH}/{applicationName}/{fileName}");
			_gitRepositoryProvider.CommitAndPush($"Added Application Configuration: {applicationName}");
		}

		/// <inheritdoc />
		public ConfigurationModel Get(string applicationName)
		{
			var repositoryPath = Config.RepositoryPath;
			var fileName = $"{applicationName}.json";
			var absolutPath = $"{repositoryPath}/{CONFIGURATIONS_PATH}/{applicationName}/{fileName}";

			using (var file = File.OpenText(absolutPath))
			{
				var serializer = new JsonSerializer();

				var result = serializer.Deserialize<ConfigurationModel>(new JsonTextReader(file));

				return result;
			}
		}

		/// <inheritdoc />
		public ConfigurationModel GetPublished(string applicationName, string version)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc />
		public void AddVariable(string applicationName, string variableName)
		{
			var config = Get(applicationName);

			config.Variables.Add(variableName);

			var repositoryPath = Config.RepositoryPath;
			var directory = Path.Combine(repositoryPath, CONFIGURATIONS_PATH, applicationName);
			var fileName = $"{applicationName}.json";
			var absolutePath = Path.Combine(directory, fileName);
			using (var file = File.CreateText(absolutePath))
			{
				var serializer = new JsonSerializer();

				serializer.Serialize(file, config);
			}

			_gitRepositoryProvider.Add($"{CONFIGURATIONS_PATH}/{applicationName}/{fileName}");
			_gitRepositoryProvider.CommitAndPush($"Added Variable: {variableName} to Configuration: {applicationName}");
		}

		/// <inheritdoc />
		public void AddVariables(string applicationName, IEnumerable<string> variableNames)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc />
		public void Publish(string applicationName, string version)
		{
			var config = Get(applicationName);

			config.Version = version;

			var repositoryPath = Config.RepositoryPath;
			var configurationDirectory = Path.Combine(repositoryPath, CONFIGURATIONS_PATH, applicationName);
			var versionDirectory = Path.Combine(configurationDirectory, version);

			Directory.CreateDirectory(versionDirectory);

			var configFileName = $"{applicationName}.json";
			
			var absoluteConfigPath = Path.Combine(versionDirectory, configFileName);



			using (var file = File.CreateText(absoluteConfigPath))
			{
				var serializer = new JsonSerializer();

				serializer.Serialize(file, config);
			}

			foreach (var variableName in config.Variables)
			{
				var variableFileName = $"{variableName}.json";
				var absoluteVariablePath = Path.Combine(versionDirectory, variableFileName);

				using (var file = File.CreateText(absoluteVariablePath))
				{
					var serializer = new JsonSerializer();

					serializer.Serialize(file, config);
				}
			}

			_gitRepositoryProvider.AddAll();
			_gitRepositoryProvider.CommitAndPush($"Published Application Configuration: {applicationName}, Version: {version}");
		}

		/// <inheritdoc />
		public void Build(string applicationName, string version)
		{
			throw new System.NotImplementedException();
		}
	}



	public struct Version
	{
		public int Major { get; set; }

		public int Minor { get; set; }

		public int Patch { get; set; }

		public Version(string version)
		{
			var versionSegments = version.Split('.');

			Major = int.Parse(versionSegments[0]);
			Minor = int.Parse(versionSegments[1]);
			Patch = int.Parse(versionSegments[2]);
		}

		public override string ToString()
		{
			return $"{Major}.{Minor}.{Patch}";
		}
	}
}

