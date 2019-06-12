using Configuration.WebApi.Services;
using Xunit;

namespace Configuration.UnitTests
{
	public class CrapUnitTests
	{

		[Fact]
		public void Read()
		{
			var crap = new Crap();

			crap.Read();
		}
	}
}
