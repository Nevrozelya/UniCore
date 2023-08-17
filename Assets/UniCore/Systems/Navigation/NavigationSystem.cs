using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniCore.Extensions;
using UniCore.Systems.Navigation.Collections;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public class NavigationSystem : IDisposable
    {
        public NavigationStack MainScenes { get; private set; }
        public NavigationGroup ContextScenes { get; private set; }
        public NavigationGroup TransitionScenes { get; private set; }

        private CancellationTokenSource _mainToken;
        private CancellationTokenSource _contextToken;

        public NavigationSystem(NavigationSetup setup)
        {
            Scene[] currentlyLoaded = NavigationUtils.GetLoadedScenes();

            if (setup != null)
            {
                MainScenes = new(setup.MainSceneNames, currentlyLoaded, allowCurrentScenePush: setup.AllowMainSceneOverride);
                ContextScenes = new(setup.ContextSceneNames, currentlyLoaded, autoLoad: setup.AutoLoadContext);
                TransitionScenes = new(setup.TransitionSceneNames, currentlyLoaded, autoLoad: false);
            }
        }

        public void Dispose()
        {
            _mainToken.CancelAndDispose();
            _contextToken.CancelAndDispose();
        }

        #region Main Scenes
        public void PushMainScene(string sceneName, object bundle = null)
        {
            PushMainSceneAwaitable(sceneName, bundle).Forget();
        }

        public UniTask PushMainSceneAwaitable(string sceneName, object bundle = null)
        {
            _mainToken.CancelAndDispose();
            _mainToken = new();
            return MainScenes.PushAsync(sceneName, bundle, _mainToken.Token);
        }

        public void PopMainScene()
        {
            PopMainSceneAwaitable().Forget();
        }

        public UniTask PopMainSceneAwaitable()
        {
            _mainToken.CancelAndDispose();
            _mainToken = new();
            return MainScenes.PopAsync(_mainToken.Token);
        }

        public void ReplaceMainScene(string sceneName, object bundle = null, bool unloadFirst = false)
        {
            ReplaceMainSceneAwaitable(sceneName, bundle, unloadFirst).Forget();
        }

        public UniTask ReplaceMainSceneAwaitable(string sceneName, object bundle = null, bool unloadFirst = false)
        {
            _mainToken.CancelAndDispose();
            _mainToken = new();
            return MainScenes.ReplaceAsync(sceneName, bundle, unloadFirst, _mainToken.Token);
        }
        #endregion

        #region Context Scenes
        public void AddContextScene(string sceneName, object bundle = null)
        {
            AddContextSceneAwaitable(sceneName, bundle).Forget();
        }

        public UniTask AddContextSceneAwaitable(string sceneName, object bundle = null)
        {
            _contextToken ??= new();
            return ContextScenes.AddAsync(sceneName, bundle, _contextToken.Token);
        }

        public void RemoveContextScene(string sceneName)
        {
            RemoveContextSceneAwaitable(sceneName).Forget();
        }

        public UniTask RemoveContextSceneAwaitable(string sceneName)
        {
            _contextToken ??= new();
            return ContextScenes.RemoveAsync(sceneName, _contextToken.Token);
        }
        #endregion
    }
}
