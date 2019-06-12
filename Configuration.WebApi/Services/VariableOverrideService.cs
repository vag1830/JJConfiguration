using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.WebApi.Services
{
	public interface IVariableOverrideService
	{
		Task<string> Get(string variableName, string key);

		Task Add(string variableName, KeyValuePair<string, string> keyValuePair);

		Task UpdateOrAdd(string variableName, KeyValuePair<string, string> keyValuePair);

		Task Delete(string variableName, KeyValuePair<string, string> keyValuePair);
	}


	public class VariableOverrideService : IVariableOverrideService
	{
		/// <inheritdoc />
		public Task<string> Get(string variableName, string key)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc />
		public Task Add(string variableName, KeyValuePair<string, string> keyValuePair)
		{
			


			throw new System.NotImplementedException();
		}

		/// <inheritdoc />
		public Task UpdateOrAdd(string variableName, KeyValuePair<string, string> keyValuePair)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc />
		public Task Delete(string variableName, KeyValuePair<string, string> keyValuePair)
		{
			throw new System.NotImplementedException();
		}
	}
}
