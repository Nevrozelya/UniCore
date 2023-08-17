using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniCore.Extensions;
using UniRx;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation.Collections
{
    public class NavigationStack : BaseNavigationCollection, IDisposable
    {
        public IReadOnlyReactiveProperty<NavigationEntry> Current => _current.ToReadOnlyReactiveProperty();
        public int StackSize => _stack.Count;

        private ReactiveProperty<NavigationEntry> _current;
        private Stack<NavigationEntry> _stack;
        private CancellationTokenSource _initialUnloadToken;
        private bool _initialSceneFound;

        // Is used to know if this stack allows
        // a push with the same name as the current scene.
        // If this happen, it either returns, or
        // override the current scene (with its bundle)
        // in order to have only 1 instance
        // of a given valid scene in the stack.
        private bool _allowCurrentScenePush;

        public NavigationStack(string[] validScenes, Scene[] loadedScenes, bool allowCurrentScenePush = false) : base(validScenes, loadedScenes)
        {
            // NOTE: Done after SetInitiallyLoadedScene() calls
            _allowCurrentScenePush = allowCurrentScenePush;
            _stack ??= new();
        }

        public void Dispose()
        {
            _initialUnloadToken.CancelAndDispose();
            _current.Dispose();
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
                Apply(scene.Value, sceneToLoad.Bundle, push: false);
            }
            else
            {
                _stack.Push(sceneToUnload); // Not unloaded because scene loading failed
            }

            return scene.HasValue;
        }

        public async UniTask<bool> PushAsync(string sceneName, object bundle, CancellationToken token)
        {
            if (!IsValid(sceneName))
            {
                return false;
            }

            Scene? scene = null;

            if (_stack.Count == 0)
            {
                scene = await NavigationUtils.LoadAsync(sceneName, token);
            }
            else if (_current.HasValue)
            {
                if (_current.Value.SceneName == sceneName)
                {
                    if (_allowCurrentScenePush)
                    {
                        // Just pop the current scene to override it, the Apply() will push the loaded scene!
                        _stack.Pop();
                    }
                    else
                    {
                        _log.Warning("Pushing the currently loaded scene is not permitted, create the stack with allowCurrentScenePush=true if you want to to allow it!");
                        return false;
                    }
                }

                string sceneToUnloadName = _current.Value.SceneName;
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
            return await ReplaceAsync(sceneName, bundle, unloadFirst: false, token);
        }

        public async UniTask<bool> ForceReplaceAsync(string sceneName, object bundle, CancellationToken token)
        {
            return await ReplaceAsync(sceneName, bundle, unloadFirst: true, token);
        }

        public async UniTask<bool> ReplaceAsync(string sceneName, object bundle, bool unloadFirst, CancellationToken token)
        {
            if (!IsValid(sceneName))
            {
                return false;
            }
            _log.Info("replace");
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

        private void Apply(Scene scene, object bundle, bool push = true)
        {
            NavigationEntry entry = new(scene, bundle);

            if (push)
            {
                _stack.Push(entry);
            }
            _current.Value = entry;

            SceneManager.SetActiveScene(scene);
        }

        protected override void SetInitiallyLoadedScene(Scene scene)
        {
            if (_initialSceneFound)
            {
                _initialUnloadToken ??= new();
                NavigationUtils.UnloadAsyc(scene.name, _initialUnloadToken.Token).Forget();
            }
            else
            {
                _initialSceneFound = true;

                NavigationEntry entry = new(scene);
                _current = new(entry);

                _stack = new();
                _stack.Push(entry);
            }
        }

        public override string ToString()
        {
            List<string> list = _stack.Select(e => e.ToString()).ToList();
            string message = $"Stack({list.Count}): {string.Join(", ", list)}";
            return message;
        }
    }
}
