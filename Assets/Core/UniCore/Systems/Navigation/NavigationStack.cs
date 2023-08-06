using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniCore.Utils;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public class NavigationStack : IDisposable
    {
        public NavigationEntry Current { get; private set; }

        private Stack<NavigationEntry> _stack;
        private HashSet<string> _mainSceneNames;
        private Logg _log;

        public NavigationStack(HashSet<string> mainSceneNames)
        {
            _log = new(this);
            _stack = new();
            _mainSceneNames = mainSceneNames;

            Scene? current = CurrentMainScene();

            if (current.HasValue)
            {
                Current = new(current.Value, null);
            }
            else
            {
                _log.Error("No current main scene found at start!");
            }
        }

        private Scene? CurrentMainScene()
        {
            if (SceneManager.sceneCount == 1)
            {
                return SceneManager.GetActiveScene();
            }
            else
            {
                int countLoaded = SceneManager.sceneCount;
                for (int i = 0; i < countLoaded; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (_mainSceneNames.Contains(scene.name))
                    {
                        return scene;
                    }
                }
            }
            return null;
        }

        public void Dispose()
        {
        }

        public async UniTask PushAsync(string sceneName, object bundle, CancellationToken token)
        {
            if (_stack.Count < 1)
            {
                return;
            }

            Scene? scene = await NavigationUtils.SwapAsync(sceneName, Current.Scene.name, token);

            if (scene.HasValue)
            {
                Current = new(scene.Value, bundle);
                _stack.Push(Current);
            }
        }

        public async UniTask PopAsync(CancellationToken token)
        {
            if (_stack.Count <= 1)
            {
                return;
            }

            NavigationEntry sceneToUnload = _stack.Pop();
            NavigationEntry sceneToLoad = _stack.Peek();

            Scene? scene = await NavigationUtils.SwapAsync(sceneToLoad.Scene.name, sceneToUnload.Scene.name, token);
        }
    }
}
