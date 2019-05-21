using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Functional.Maybe.Tests
{
	[TestClass]
	public class MaybeNullableTests
	{
		[TestMethod]
		public void ToNullableTest()
		{
			var nothing = Maybe<Guid>.Nothing;
			Assert.AreEqual(null, nothing.ToNullable());
		}
	}
}
