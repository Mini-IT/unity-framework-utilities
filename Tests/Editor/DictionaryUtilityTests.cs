using System.Collections.Generic;
using MiniIT.Utils;
using NUnit.Framework;

namespace MiniIT.Framework.Utilities.Tests
{
	public class DictionaryUtilityTests
	{
		[Test]
		public void TryGetValue_ReturnsTrue_WhenTypeMatches()
		{
			var dict = new Dictionary<string, object>
			{
				{ "key", 42 }
			};

			var success = DictionaryUtility.TryGetValue<int>(dict, "key", out var value);

			Assert.IsTrue(success);
			Assert.AreEqual(42, value);
		}

		[Test]
		public void TryGetValue_ConvertsStringToInt_WhenPossible()
		{
			var dict = new Dictionary<string, object>
			{
				{ "key", "123" }
			};

			var success = DictionaryUtility.TryGetValue<int>(dict, "key", out var value);

			Assert.IsTrue(success);
			Assert.AreEqual(123, value);
		}

		[Test]
		public void TryGetValue_DoesNotThrow_ForStringThreeToInt()
		{
			var dict = new Dictionary<string, object>
			{
				{ "key", "3" }
			};

			Assert.DoesNotThrow(() =>
			{
				var success = DictionaryUtility.TryGetValue<int>(dict, "key", out var value);
				Assert.IsTrue(success);
				Assert.AreEqual(3, value);
			});
		}

		[Test]
		public void TryGetValue_ConvertsNumericType_WhenPossible()
		{
			var dict = new Dictionary<string, object>
			{
				{ "key", 5L }
			};

			var success = DictionaryUtility.TryGetValue<int>(dict, "key", out var value);

			Assert.IsTrue(success);
			Assert.AreEqual(5, value);
		}

		[Test]
		public void TryGetValue_ReturnsFalse_WhenKeyMissing()
		{
			var dict = new Dictionary<string, object>();

			var success = DictionaryUtility.TryGetValue<int>(dict, "missing", out var value);

			Assert.IsFalse(success);
			Assert.AreEqual(0, value);
		}

		[Test]
		public void TryGetValue_ReturnsFalse_WhenConversionFails()
		{
			var dict = new Dictionary<string, object>
			{
				{ "key", "not_an_int" }
			};

			var success = DictionaryUtility.TryGetValue<int>(dict, "key", out var value);

			Assert.IsFalse(success);
			Assert.AreEqual(0, value);
		}

		[Test]
		public void TryGetValue_ReturnsTrue_WhenNullReferenceType()
		{
			var dict = new Dictionary<string, object>
			{
				{ "key", null }
			};

			var success = DictionaryUtility.TryGetValue<string>(dict, "key", out var value);

			Assert.IsTrue(success);
			Assert.IsNull(value);
		}
	}
}
