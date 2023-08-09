using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public static class NavigationUtils
    {
        public static async UniTask<Scene?> SwapLoadAsync(string sceneToLoadName, string sceneToUnloadName, CancellationToken token)
        {
            Scene? loadedScene = await LoadAsync(sceneToLoadName, token);

            if (loadedScene.HasValue)
            {
                await UnloadAsyc(sceneToUnloadName, token);
            }

            return loadedScene;
        }

        public static async UniTask<Scene?> SwapUnloadAsync(string sceneToLoadName, string sceneToUnloadName, CancellationToken token)
        {
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

            if (task.Status == UniTaskStatus.Succeeded && !token.IsCancellationRequested)
            {
                await UniTask.WaitUntil(() => result.HasValue, cancellationToken: token);
            }

            SceneManager.sceneLoaded -= callback;

            return result;
        }

        public static async UniTask<bool> UnloadAsyc(string sceneToUnloadName, CancellationToken token)
        {
            UniTask task = SceneManager.UnloadSceneAsync(
                sceneToUnloadName,
                UnloadSceneOptions.UnloadAllEmbeddedSceneObjects)
            .WithCancellation(token);

            await task;
            return task.Status == UniTaskStatus.Succeeded;
        }
    }
}
