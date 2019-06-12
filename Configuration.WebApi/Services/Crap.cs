using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Configuration.Cli;
using Configuration.WebApi.Providers;
using Newtonsoft.Json;

namespace Configuration.WebApi.Services
{
	public interface IVariableService
	{
		IEnumerable<Variable> GetAllVariables();

		Variable GetVariableByName(string name);

		Variable AddVariable(Variable variable);

		Task<Variable> UpdateVariable(Variable variable);

		Task<Variable> DeleteVariable(string name);
	}

	public class VariableService : IVariableService
	{
		private const string VARIABLES_PATH = "variables";

		private readonly IGitRepositoryProvider _gitRepositoryProvider = new GitRepositoryProvider();
		
		public static readonly List<Variable> Variables = new List<Variable>
		{
			new Variable
			{
				Name = "WalletEndpointAddress",
				Value = "https://xxx.bde.com/walletservice",
				Overrides = new Dictionary<string, string>
				{
					{"WalletEndpointAddress.qa.betsson.desktop", "https://xxx.betsson.bde.com/walletservice"},
					{"WalletEndpointAddress.qa.betsafe.desktop", "https://xxx.betsafe.bde.com/walletservice"}
				}
			}
		};

		/// <inheritdoc />
		public IEnumerable<Variable> GetAllVariables()
		{
			var repositoryPath = Config.RepositoryPath;
			var directoryPath = $"{repositoryPath}/{VARIABLES_PATH}";

			var directoryInfo = new DirectoryInfo(directoryPath);

			var files = directoryInfo.GetFiles();

			var variables = files.Select(file => GetVariableByName(Path.GetFileNameWithoutExtension(file.FullName)));

			return variables;
		}

		/// <inheritdoc />
		public Variable GetVariableByName(string name)
		{
			var repositoryPath = Config.RepositoryPath;
			var fileName = $"{name}.json";
			var absolutPath = $"{repositoryPath}/{VARIABLES_PATH}/{fileName}";

			using (var file = File.OpenText(absolutPath))
			{
				var serializer = new JsonSerializer();

				var result = serializer.Deserialize<Variable>(new JsonTextReader(file));

				return result;
			}
		}

		/// <inheritdoc />
		public Variable AddVariable(Variable variable)
		{
			var repositoryPath = Config.RepositoryPath;
			var fileName = $"{variable.Name}.json";
			var absolutePath = $"{repositoryPath}/{VARIABLES_PATH}/{fileName}";

			using (var file = File.CreateText(absolutePath))
			{
				var serializer = new JsonSerializer();

				serializer.Serialize(file, variable);
			}

			_gitRepositoryProvider.Add($"{VARIABLES_PATH}/{fileName}");
			_gitRepositoryProvider.CommitAndPush($"Added Variable: {variable.Name}");

			return variable;
		}

		/// <inheritdoc />
		public Task<Variable> UpdateVariable(Variable variable)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc />
		public Task<Variable> DeleteVariable(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}
