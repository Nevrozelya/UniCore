using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public class NavigationGroup : BaseNavigationCollection, IDisposable
    {
        public ReactiveCollection<NavigationEntry> Currents { get; private set; }

        public NavigationGroup(params string[] existings) : base(existings)
        {
            Currents = new();
        }

        public void Dispose()
        {
            Currents.Dispose();
        }

        public async UniTask AddAsync(string sceneName, object bundle, CancellationToken token)
        {
            if (!IsExisting(sceneName))
            {
                return;
            }

            if (Currents.Any(s => s.SceneName == sceneName))
            {
                return;
            }

            Scene? scene = await NavigationUtils.LoadAsync(sceneName, token);

            if (scene.HasValue)
            {
                NavigationEntry entry = new(scene.Value, bundle);
                Currents.Add(entry);
            }
        }

        public async UniTask RemoveAsync(string sceneName, CancellationToken token)
        {
            NavigationEntry existing = Currents.FirstOrDefault(s => s.SceneName == sceneName);

            if (existing == null)
            {
                return;
            }

            bool isUnloaded = await NavigationUtils.UnloadAsyc(sceneName, token);

            if (isUnloaded)
            {
                Currents.Remove(existing);
            }

            // Note : we prefere iterate twice (FirstOrDefault & Remove)
            // than create a temp list to RemoveAt index with
            // int index = Currents.ToList().FindIndex(s => s.SceneName == sceneName);
            // because we won't ever have insanely high numbers on scenes
            // loaded at the same time, so CPU > RAM here.
        }
    }
}
