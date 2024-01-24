using System;
using System.Collections.Generic;
using System.Globalization;

namespace MiniIT.Utils
{
	public static class DictionaryUtility
	{
		/// <summary>
		/// Gets the value of the specified key from the given dictionary and converts it to the specified type if needed.
		/// If the key is not found or the dictionary is null, it returns the default value.
		/// </summary>
		/// <typeparam name="T">The type of the value to return.</typeparam>
		/// <param name="dict">The dictionary from which to get the value.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="defaultValue">The default value to return if the key is not found or the dictionary is null.</param>
		/// <returns>The value of the specified key, or the default value if the key is not found or the dictionary is null.</returns>
		public static T GetValue<T>(IDictionary<string, object> dict, string key, T defaultValue = default)
		{
			if (dict == null || !dict.ContainsKey(key))
			{
				return defaultValue;
			}

			var result_type = typeof(T);
			if (result_type == typeof(bool))
			{
				var string_value = SafeGetValue<string>(dict, key);
				if (string.IsNullOrEmpty(string_value))
				{
					return defaultValue;
				}

				if (int.TryParse(string_value, out int int_value))
				{
					return (T)Convert.ChangeType(int_value, result_type, CultureInfo.InvariantCulture);
				}
				if (double.TryParse(string_value, out double double_value))
				{
					return (T)Convert.ChangeType(double_value, result_type, CultureInfo.InvariantCulture);
				}

				return (T)Convert.ChangeType(string_value, result_type, CultureInfo.InvariantCulture);
			}

			return SafeGetValue<T>(dict, key, defaultValue);
		}

		/// <summary>
		/// Tries to get the value associated with the specified key from the dictionary and convert it to the specified type.
		/// </summary>
		/// <typeparam name="T">The type to convert the value to.</typeparam>
		/// <param name="dict">The dictionary to search for the specified key.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
		/// <returns>true if the dictionary contains an element with the specified key and the value can be converted to the specified type; otherwise, false.</returns>
		public static bool TryGetValue<T>(IDictionary<string, object> dict, string key, out T value)
		{
			if (dict.TryGetValue(key, out var result))
			{
				try
				{
					value = (T)result;
					return true;
				}
				catch (InvalidCastException)
				{
					try
					{
						value = (T)Convert.ChangeType(result, typeof(T), CultureInfo.InvariantCulture);
						return true;
					}
					catch (Exception)
					{
					}
				}
				catch (NullReferenceException) // field exists but `result` is null
				{
				}
			}

			value = default;
			return false;
		}

		/// <summary>
		/// Tries to get the value associated with the specified key from the dictionary and convert it to the specified type.
		/// If it fails, then <paramref name="defaultValue"/> will be returned.
		/// </summary>
		/// <typeparam name="T">The type of the value to get.</typeparam>
		/// <param name="dict">The dictionary to search for the specified key.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="defaultValue">The default value to return if the key is not found or the dictionary is null
		/// or if the result value can not be casted to the specified type.</param>
		/// <returns>The value of the specified key, or the default value if the key is not found or the dictionary is null
		/// or if the result value can not be casted to the specified type.</returns>
		public static T SafeGetValue<T>(IDictionary<string, object> dict, string key, T defaultValue = default)
		{
			if (TryGetValue<T>(dict, key, out var result))
			{
				return result;
			}
			return defaultValue;
		}

		/// <summary>
		/// Inserts all values from <paramref name="source"/> to <paramref name="destination"/>
		/// </summary>
		/// <param name="destination">A dictionary to insert values into</param>
		/// <param name="source">A dictionary which values are going to be inserted into <paramref name="destination"/></param>
		public static void Merge(IDictionary<string, object> destination, IDictionary<string, object> source)
		{
			foreach (var item in source)
			{
				destination[item.Key] = item.Value;
			}
		}
	}
}
