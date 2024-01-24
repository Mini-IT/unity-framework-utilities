using UnityEngine;

namespace MiniIT.Unity
{
	public class TransformUtility
	{
		public static void DestroyChildren(Transform transform)
		{
			if (transform == null)
				return;

			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
			}
		}
	}
}
