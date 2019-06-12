using Configuration.WebApi.Services;
using Xunit;

namespace Configuration.UnitTests
{
	public class ApplicationConfigurationUnitTests
	{
		private readonly ApplicationConfigurationService _sut = new ApplicationConfigurationService();

		[Fact]
		public void Add() 
		{
			_sut.Add("CommonBaseApi");
			
		}

		[Fact]
		public void GetByName() 
		{
			var actual = _sut.Get("CommonBaseApi");

			Assert.Equal("CommonBaseApi", actual.ApplicationName);
			
		}

		[Fact]
		public void AddVariable() 
		{
			_sut.AddVariable("CommonBaseApi", "PaymentsServiceEndpointAddress3");

			var actual = _sut.Get("CommonBaseApi");

			Assert.Contains("WalletEndpointAddress", actual.Variables);
		}

		[Fact]
		public void Publish() 
		{
			_sut.Publish("CommonBaseApi", "1.0.1");

			var actual = _sut.Get("CommonBaseApi");

			Assert.Contains("WalletEndpointAddress", actual.Variables);
		}
	}
}
