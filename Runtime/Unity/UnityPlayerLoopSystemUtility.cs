using System;
using UnityEngine;
using UnityEngine.LowLevel;

namespace MiniIT.Unity
{
	public static class UnityPlayerLoopSystemUtility
	{
		public static void AddLoopSystem(PlayerLoopSystem loop)
		{
			PlayerLoopSystem loopSystems = PlayerLoop.GetCurrentPlayerLoop();

			Array.Resize(ref loopSystems.subSystemList, loopSystems.subSystemList.Length + 1);
			loopSystems.subSystemList[^1] = loop;

			PlayerLoop.SetPlayerLoop(loopSystems);
		}

		public static void RemoveLoopSystem(PlayerLoopSystem loop)
		{
			PlayerLoopSystem loopSystems = PlayerLoop.GetCurrentPlayerLoop();

			int index = Array.IndexOf(loopSystems.subSystemList, loop);
			if (index >= 0)
			{
				var list = new PlayerLoopSystem[loopSystems.subSystemList.Length - 1];
				Array.ConstrainedCopy(loopSystems.subSystemList, 0, list, 0, index);
				Array.ConstrainedCopy(loopSystems.subSystemList, index + 1, list, index, loopSystems.subSystemList.Length - index - 1);
				loopSystems.subSystemList = list;

				PlayerLoop.SetPlayerLoop(loopSystems);
			}
		}
	}
}
