using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public static class NavigationUtils
    {
        public static async UniTask<Scene?> SwapLoadAsync(string sceneToLoadName, string sceneToUnloadName, CancellationToken token)
        {
            if (string.IsNullOrEmpty(sceneToLoadName))
            {
                return null;
            }

            if (string.IsNullOrEmpty(sceneToUnloadName))
            {
                return null;
            }

            Scene? loadedScene = await LoadAsync(sceneToLoadName, token);

            if (loadedScene.HasValue)
            {
                await UnloadAsyc(sceneToUnloadName, token);
            }

            return loadedScene;
        }

        public static async UniTask<Scene?> SwapUnloadAsync(string sceneToLoadName, string sceneToUnloadName, CancellationToken token)
        {
            if (string.IsNullOrEmpty(sceneToLoadName))
            {
                return null;
            }

            if (string.IsNullOrEmpty(sceneToUnloadName))
            {
                return null;
            }

            Scene? loadedScene = null;

            bool isUnloaded = await UnloadAsyc(sceneToUnloadName, token);

            if (isUnloaded)
            {
                loadedScene = await LoadAsync(sceneToLoadName, token);
            }

            return loadedScene;
        }

        public static async UniTask<Scene?> LoadAsync(string sceneToLoadName, CancellationToken token)
        {
            if (string.IsNullOrEmpty(sceneToLoadName))
            {
                return null;
            }

            UniTask task = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive).WithCancellation(token);

            Scene? result = null;

            UnityAction<Scene, LoadSceneMode> callback = (s, _) =>
            {
                if (s.name == sceneToLoadName) // Might not be the case if another scene in loaded in the same time
                {
                    result = s;
                }
            };

            SceneManager.sceneLoaded += callback;

            await task;

            float timeout = 5f; // Seconds
            while (!result.HasValue && timeout > 0 && !token.IsCancellationRequested)
            {
                timeout -= UnityEngine.Time.deltaTime;
                await UniTask.Yield();
            }

            SceneManager.sceneLoaded -= callback;

            return result;
        }

        public static async UniTask<bool> UnloadAsyc(string sceneToUnloadName, CancellationToken token)
        {
            if (string.IsNullOrEmpty(sceneToUnloadName))
            {
                return false;
            }

            UnityEngine.AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(
                sceneToUnloadName,
                UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

            await asyncOp.WithCancellation(token);
            return asyncOp.isDone;
        }

        public static Scene[] GetLoadedScenes()
        {
            List<Scene> scenes = new();
            int count = SceneManager.loadedSceneCount;

            for (int i = 0; i < count; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                scenes.Add(scene);
            }

            return scenes.ToArray();
        }
    }
}
