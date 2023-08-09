using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;

namespace UniCore.Systems.Navigation
{
    public class NavigationGroup : BaseNavigationCollection, IDisposable
    {
        public ReactiveCollection<NavigationEntry> Currents { get; private set; }

        private Dictionary<string, NavigationEntry> _dictionary;

        public NavigationGroup(params string[] existings) : base(existings)
        {
            _dictionary = new();
            Currents = new();
        }

        public void Dispose()
        {
            Currents.Dispose();
        }

        public async UniTask AddAsync(string sceneName, object bundle, CancellationToken token)
        {
            // TODO
        }

        public async UniTask RemoveAsync(string sceneName, CancellationToken token)
        {
            // TODO
        }
    }
}
