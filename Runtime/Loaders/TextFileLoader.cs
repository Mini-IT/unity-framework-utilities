#if (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
#define SA_WEBREQUEST
#endif

using System;
using System.IO;

#if SA_WEBREQUEST
using UnityEngine.Networking;
#endif

#if UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace MiniIT.Utils
{
	public class TextFileLoader
	{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#if UNITASK
		public async UniTask<string> ReadTextFromFile(string path)
#else
		public async Task<string> ReadTextFromFile(string path)
#endif
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			if (!File.Exists(path))
			{
				return null;
			}

			string text = null;
			try
			{
#if UNITY_2021_1_OR_NEWER
				text = await File.ReadAllTextAsync(path);
#else
				text = File.ReadAllText(path);
#endif
			}
			catch (Exception) { }

			return text;
		}

#if SA_WEBREQUEST

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#if UNITASK
		public async UniTask<string> ReadTextFromStreamingAssets(string path)
#else
		public async Task<string> ReadTextFromStreamingAssets(string path)
#endif
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			using (var request = new UnityWebRequest(path))
			{
				request.downloadHandler = new DownloadHandlerBuffer();
				var op = request.SendWebRequest();
#if UNITASK
				await op.ToUniTask();
#else
				while (!op.isDone)
				{
					await Task.Delay(10);
				}
#endif
				if (request.result != UnityWebRequest.Result.Success)
				{
					return null;
				}

				return request.downloadHandler.text;
			}

			return null;
		}

#else // SA_WEBREQUEST

#if UNITASK
		public UniTask<string> ReadTextFromStreamingAssets(string path) => ReadTextFromFile(path);
#else
		public Task<string> ReadTextFromStreamingAssets(string path) => ReadTextFromFile(path);
#endif

#endif // SA_WEBREQUEST
	}
}
