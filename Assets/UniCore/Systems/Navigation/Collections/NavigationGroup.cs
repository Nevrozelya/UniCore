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

        public NavigationGroup(string[] validScenes, Scene[] loadedScenes, bool autoLoad = false) : base(validScenes, loadedScenes)
        {
            // NOTE: Done after SetInitiallyLoadedScene() calls

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
            _current.Dispose();
        }

        public async UniTask AddAsync(string sceneName, object bundle, CancellationToken token)
        {
            if (!IsValid(sceneName))
            {
                _log.Error($"Given scene name ({sceneName}) is not valid!");
                return;
            }

            if (_current.Any(s => s.SceneName == sceneName))
            {
                _log.Warning($"Given scene ({sceneName}) is already loaded!");
                return;
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

            // Note : we prefere iterate twice (FirstOrDefault & Remove)
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
