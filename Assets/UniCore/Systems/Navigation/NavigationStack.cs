using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniCore.Extensions;
using UniRx;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public class NavigationStack : BaseNavigationCollection, IDisposable
    {
        public ReactiveProperty<NavigationEntry> Current { get; private set; }

        private Stack<NavigationEntry> _stack;
        private CancellationTokenSource _initUnloadToken;

        public NavigationStack(params string[] existings) : base(existings)
        {
            _stack = new();

            NavigationEntry entry = null;
            Scene? scene = GetFirstExistingAndUnloadOthers();

            if (scene.HasValue)
            {
                entry = new(scene.Value);
            }
            else
            {
                _log.Error("No current main scene found at start!");
            }

            Current = new(entry);
        }

        public void Dispose()
        {
            _initUnloadToken.CancelAndDispose();
            Current.Dispose();
        }

        public async UniTask<bool> PopAsync(CancellationToken token)
        {
            if (_stack.Count <= 1)
            {
                _log.Error("Can't pop a stack to or from emptiness!");
                return false;
            }

            NavigationEntry sceneToUnload = _stack.Pop();
            NavigationEntry sceneToLoad = _stack.Peek();

            Scene? scene = await NavigationUtils.SwapLoadAsync(sceneToLoad.SceneName, sceneToUnload.SceneName, token);

            if (scene.HasValue)
            {
                Apply(scene.Value, sceneToLoad.Bundle);
            }
            else
            {
                _stack.Push(sceneToUnload); // Not unloaded because scene loading failed
            }

            return scene.HasValue;
        }

        public async UniTask<bool> PushAsync(string sceneName, object bundle, CancellationToken token)
        {
            if (!IsExisting(sceneName))
            {
                return false;
            }

            Scene? scene = null;

            if (_stack.Count == 0)
            {
                scene = await NavigationUtils.LoadAsync(sceneName, token);
            }
            else if (Current.HasValue)
            {
                string sceneToUnloadName = Current.Value.SceneName;
                scene = await NavigationUtils.SwapLoadAsync(sceneName, sceneToUnloadName, token);
            }

            if (scene.HasValue)
            {
                Apply(scene.Value, bundle);
            }

            return scene.HasValue;
        }

        public async UniTask<bool> SoftReplaceAsync(string sceneName, object bundle, CancellationToken token)
        {
            return await ReplaceAsync(unloadFirst: false, sceneName, bundle, token);
        }

        public async UniTask<bool> ForceReplaceAsync(string sceneName, object bundle, CancellationToken token)
        {
            return await ReplaceAsync(unloadFirst: true, sceneName, bundle, token);
        }

        private async UniTask<bool> ReplaceAsync(bool unloadFirst, string sceneName, object bundle, CancellationToken token)
        {
            if (!IsExisting(sceneName))
            {
                return false;
            }

            Scene? scene;

            if (_stack.Count == 0)
            {
                scene = await NavigationUtils.LoadAsync(sceneName, token);
            }
            else
            {
                NavigationEntry sceneToUnload = _stack.Pop();

                if (unloadFirst)
                {
                    scene = await NavigationUtils.SwapUnloadAsync(sceneName, sceneToUnload.SceneName, token);
                }
                else
                {
                    scene = await NavigationUtils.SwapLoadAsync(sceneName, sceneToUnload.SceneName, token);
                }

                if (!scene.HasValue)
                {
                    _stack.Push(sceneToUnload); // Not unloaded because scene loading failed
                }
            }

            if (scene.HasValue)
            {
                Apply(scene.Value, bundle);
            }

            return scene.HasValue;
        }

        private void Apply(Scene scene, object bundle)
        {
            NavigationEntry entry = new(scene, bundle);
            _stack.Push(entry);
            Current.Value = entry;

            SceneManager.SetActiveScene(scene);
        }

        private Scene? GetFirstExistingAndUnloadOthers()
        {
            if (SceneManager.sceneCount == 1)
            {
                return SceneManager.GetActiveScene();
            }
            else if (!_existingSceneNames.IsNullOrEmpty())
            {
                Scene? result = null;
                int countLoaded = SceneManager.sceneCount;

                for (int i = 0; i < countLoaded; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);

                    if (_existingSceneNames.Contains(scene.name))
                    {
                        if (!result.HasValue)
                        {
                            result = scene;
                        }
                        else
                        {
                            _initUnloadToken ??= new();
                            NavigationUtils.UnloadAsyc(scene.name, _initUnloadToken.Token).Forget();
                        }
                    }
                }
            }
            return null;
        }
    }
}
