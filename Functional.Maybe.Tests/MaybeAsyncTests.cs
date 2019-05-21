using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Functional.Maybe.Tests
{
	[TestClass]
	public class MaybeAsyncTests
	{
		[TestMethod]
		public async Task SelectAsyncTest()
		{
			Task<int> two() => Task.FromResult(2);

			var onePlusTwo = await 1.ToMaybe().SelectAsync(async one => one + (await two()));

			Assert.AreEqual(3, onePlusTwo.Value);
		}
	}
}
