using UnityEngine;

namespace MiniIT.Unity
{
	public static class GameObjectExtension
	{
		public static void SetLayerRecursively(this GameObject obj, int layer)
		{
			foreach (Transform trans in obj.GetComponentsInChildren<Transform>(true))
			{
				trans.gameObject.layer = layer;
			}
		}
	}
}