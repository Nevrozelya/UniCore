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
        public Scene Scene => Current.Value?.Scene ?? default;

        private Stack<NavigationEntry> _stack;
        private CancellationTokenSource _initUnloadToken;

        public NavigationStack(params string[] existings) : base(existings)
        {
            _stack = new();

            NavigationEntry entry = null;
            Scene? scene = CurrentMainScene();

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

        private Scene? CurrentMainScene()
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

        public async UniTask PushAsync(string sceneName, object bundle, CancellationToken token)
        {
            Scene? scene;

            if (_stack.Count == 0)
            {
                scene = await NavigationUtils.LoadAsync(sceneName, token);
            }
            else
            {
                scene = await NavigationUtils.SwapLoadAsync(sceneName, Scene.name, token);
            }

            if (scene.HasValue)
            {
                Apply(scene.Value, bundle);
            }
        }

        public async UniTask PopAsync(CancellationToken token)
        {
            if (_stack.Count <= 1)
            {
                _log.Error("Can't pop a stack to or from emptiness!");
                return;
            }

            NavigationEntry sceneToUnload = _stack.Pop();
            NavigationEntry sceneToLoad = _stack.Peek();

            Scene? scene = await NavigationUtils.SwapLoadAsync(sceneToLoad.SceneName, sceneToUnload.SceneName, token);

            if (scene.HasValue)
            {
                Apply(scene.Value, sceneToLoad.Bundle);
            }
        }

        private void Apply(Scene scene, object bundle)
        {
            NavigationEntry entry = new(scene, bundle);

            _stack.Push(entry);
            Current.Value = entry;
            SceneManager.SetActiveScene(scene);
        }
    }
}
