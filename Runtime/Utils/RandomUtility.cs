using System;
using System.Collections.Generic;

namespace MiniIT.Utils
{
	public class RandomUtility
	{
		private static RandomUtility s_instance;
		public static RandomUtility Default => s_instance ??= new RandomUtility();

		private readonly Random _random;
		public Random Random => _random;

		public RandomUtility() : this(null) { }
		public RandomUtility(Random random)
		{
			_random = random ?? new Random();
		}
		public RandomUtility(int seed)
		{
			_random = new Random(seed);
		}

		public int GetWeightedRandomIndex(IList<int> weights)
		{
			int sum_weight = 0;
			for (int i = 0; i < weights.Count; i++)
			{
				sum_weight += weights[i];
			}
			int value = Range(0, sum_weight) + 1;
			int sum = 0;
			for (int i = 0; i < weights.Count; i++)
			{
				sum += weights[i];
				if (value <= sum)
				{
					return i;
				}
			}

			// should not get here
			return -1;
		}

		public T GetWeightedRandom<T>(Dictionary<T, int> weights)
		{
			int sum_weight = 0;
			foreach (int w in weights.Values)
			{
				sum_weight += w;
			}
			int value = Range(0, sum_weight) + 1;
			int sum = 0;
			foreach (var pair in weights)
			{
				sum += pair.Value;
				if (value <= sum)
				{
					return pair.Key;
				}
			}

			// should not get here
			return default;
		}

		public void Shuffle<T>(IList<T> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				int index = Range(0, list.Count);
				if (i != index)
				{
					Swap(list, i, index);
				}
			}
		}

		public static void Swap<T>(IList<T> list, int a, int b)
		{
			T temp = list[a];
			list[a] = list[b];
			list[b] = temp;
		}

		public int Range(int min, int max)
		{
			return _random.Next(min, max);
		}

		public double Range(double min, double max)
		{
			return min + _random.NextDouble() * (max - min);
		}

		public float Range(float min, float max)
		{
			return min + (float)_random.NextDouble() * (max - min);
		}
	}
}
