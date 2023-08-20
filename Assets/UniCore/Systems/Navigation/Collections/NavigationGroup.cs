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
    public class NavigationGroup : BaseNavigationCollection, IDisposable
    {
        public IReadOnlyReactiveCollection<NavigationEntry> Currents => _current;
        public int GroupSize => _current.Count;

        private ReactiveCollection<NavigationEntry> _current;
        private CancellationTokenSource _autoLoadToken;
        private CancellationTokenSource _runtimeToken;

        public NavigationGroup(string[] validScenes, Scene[] loadedScenes, NavigationCollectionConduct conduct, bool autoLoad = false) : base(validScenes, loadedScenes, conduct)
        {
            // Note: Done after SetInitiallyLoadedScene() calls

            _current ??= new();

            if (autoLoad && !validScenes.IsNullOrEmpty())
            {
                _autoLoadToken = new();

                foreach (var validScene in validScenes)
                {
                    AddAsync(validScene, null, _autoLoadToken.Token).Forget();
                }
            }
        }

        public void Dispose()
        {
            _autoLoadToken.CancelAndDispose();
            _runtimeToken.CancelAndDispose();
            _current.Dispose();
        }

        public void Add(string sceneName, object bundle = null)
        {
            AddAwaitable(sceneName, bundle).Forget();
        }

        public UniTask AddAwaitable(string sceneName, object bundle = null)
        {
            _runtimeToken ??= new();
            return AddAsync(sceneName, bundle, _runtimeToken.Token);
        }

        public void Remove(string sceneName)
        {
            RemoveAwaitable(sceneName).Forget();
        }

        public UniTask RemoveAwaitable(string sceneName)
        {
            _runtimeToken ??= new();
            return RemoveAsync(sceneName, _runtimeToken.Token);
        }

        public async UniTask AddAsync(string sceneName, object bundle, CancellationToken token)
        {
            if (!IsValid(sceneName))
            {
                return;
            }

            if (_current.Any(s => s.SceneName == sceneName))
            {
                if (_conduct == NavigationCollectionConduct.Replace)
                {
                    await RemoveAsync(sceneName, token);
                }
                else if (_conduct == NavigationCollectionConduct.Forbidden)
                {
                    _log.Warning($"Adding a currently loaded scene is not permitted, change stack conduct if you want to allow it!");
                    return;
                }
                else
                {
                    _log.Error($"Given conduct {_conduct} is not implemented, considered forbidden!");
                    return;
                }
            }

            Scene? scene = await NavigationUtils.LoadAsync(sceneName, token);

            if (scene.HasValue)
            {
                NavigationEntry entry = new(scene.Value, bundle);
                _current.Add(entry);
            }
        }

        public async UniTask RemoveAsync(string sceneName, CancellationToken token)
        {
            NavigationEntry existing = _current.FirstOrDefault(s => s.SceneName == sceneName);

            if (existing == null)
            {
                _log.Error("Given scene is not loaded!");
                return;
            }

            bool isUnloaded = await NavigationUtils.UnloadAsyc(sceneName, token);

            if (isUnloaded)
            {
                _current.Remove(existing);
            }

            // Note : We prefere iterate twice (FirstOrDefault & Remove)
            // than create a temp list to RemoveAt index with
            // int index = Currents.ToList().FindIndex(s => s.SceneName == sceneName);
            // because we won't ever have insanely high numbers on scenes
            // loaded at the same time, so CPU > RAM here.
        }

        protected override void SetInitiallyLoadedScene(Scene scene)
        {
            NavigationEntry entry = new(scene);

            _current ??= new();
            _current.Add(entry);
        }

        public override string ToString()
        {
            List<string> list = _current.Select(e => e.ToString()).ToList();
            string message = $"Stack({list.Count}): {string.Join(", ", list)}";
            return message;
        }
    }
}
