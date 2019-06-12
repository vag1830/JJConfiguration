using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Configuration.WebApi.Providers;

namespace Configuration.WebApi.Services
{

	public class Crap
	{
		private const string VARIABLES_PATH = "variables";

		private readonly IGitRepositoryProvider _gitRepositoryProvider = new GitRepositoryProvider();
		
		public void Read()
		{
			var lines = File.ReadAllLines("C:/Users/vamp/Downloads/epa-http.txt");

			foreach (var line in lines)
			{
				// do things here
			}
		}

		private List<string> ReverseStringFormat(string line)
		{
			var template = "{0} [{1}:{2}:{3}:{4}] \"{5}\" {6} {7}";

			//Handels regex special characters.
			template = Regex.Replace(template, @"[\\\^\$\.\|\?\*\+\(\)\[\]]", match => "\\" + match.Value);

			var pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";

			var r = new Regex(pattern);
			var m = r.Match(line);
			var ret = new List<string>();

			for (var i = 1; i < m.Groups.Count; i++)
			{
				ret.Add(m.Groups[i].Value);
			}

			return ret;
		}

		//public IEnumerable<Variable> GetAllVariables()
		//{
		//	var repositoryPath = Config.RepositoryPath;
		//	var directoryPath = $"{repositoryPath}/{VARIABLES_PATH}";

		//	var directoryInfo = new DirectoryInfo(directoryPath);

		//	var files = directoryInfo.GetFiles();

		//	var variables = files.Select(file => GetVariableByName(Path.GetFileNameWithoutExtension(file.FullName)));

		//	return variables;
		//}

		//public Variable AddVariable(Variable variable)
		//{
		//	var repositoryPath = Config.RepositoryPath;
		//	var fileName = $"{variable.Name}.json";
		//	var absolutePath = $"{repositoryPath}/{VARIABLES_PATH}/{fileName}";

		//	using (var file = File.CreateText(absolutePath))
		//	{
		//		var serializer = new JsonSerializer();

		//		serializer.Serialize(file, variable);
		//	}

		//	_gitRepositoryProvider.Add($"{VARIABLES_PATH}/{fileName}");
		//	_gitRepositoryProvider.CommitAndPush($"Added Variable: {variable.Name}");

		//	return variable;
		//}
	}
}
