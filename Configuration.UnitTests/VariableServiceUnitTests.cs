using System.Collections.Generic;
using System.Linq;
using Configuration.Cli;
using Configuration.WebApi.Services;
using Xunit;

namespace Configuration.UnitTests
{
	public class VariableServiceUnitTests
	{
		private readonly VariableService _sut = new VariableService();

		[Fact]
		public void AddVariable() 
		{
			var variable = new Variable
			{
				Name = "WalletEndpointAddress",
				Value = "https://www.betsson.bde.com/walletservice",
				Overrides = new Dictionary<string, string>
				{
					{"WalletEndpointAddress.qa.betsson.desktop", "https://xxx.betsson.bde.com/walletservice"},
					{"WalletEndpointAddress.qa.betsafe.desktop", "https://xxx.betsafe.bde.com/walletservice"},
					{"WalletEndpointAddress.alpha.betsson.desktop", "https://xxx.betsson.ble.com/walletservice"}
				}
			};

			var actual = _sut.AddVariable(variable);

			Assert.Equal(variable.Name, actual.Name);
		}

		[Fact]
		public void GetVariable() 
		{
			var sut = new VariableService();

			var actual = sut.GetVariableByName("WalletEndpointAddress");

			Assert.Equal("WalletEndpointAddress", actual.Name);
		}

		[Fact]
		public void GetAllVariables() 
		{
			var actual = _sut.GetAllVariables();

			Assert.Equal(2, actual.ToList().Count);

		}
	}
}
