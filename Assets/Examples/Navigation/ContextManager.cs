using System;
using UniCore.Systems.Navigation.Collections;
using UniCore.Utils;
using UniRx;
using Zenject;

namespace UniCore.Examples.Navigation
{
    public class ContextManager : IInitializable, IDisposable
    {
        [Inject] private ContextController _controller;
        [Inject] private NavigationService _navigation;

        private CompositeDisposable _disposables;
        private int _bundle;

        public void Initialize()
        {
            _disposables = new();

            _controller.PushAEvent.Subscribe(OnPushA).AddTo(_disposables);
            _controller.PushBEvent.Subscribe(OnPushB).AddTo(_disposables);
            _controller.PopEvent.Subscribe(OnPop).AddTo(_disposables);
            _controller.ReplaceAEvent.Subscribe(OnReplaceA).AddTo(_disposables);
            _controller.ReplaceBEvent.Subscribe(OnReplaceB).AddTo(_disposables);
            _controller.ResetContextEvent.Subscribe(OnResetContext).AddTo(_disposables);

            _navigation.Main.Current.Subscribe(OnMainSceneChanged).AddTo(_disposables);
        }

        private void OnMainSceneChanged(NavigationEntry entry)
        {
            Logg.Info("Navigated to : " + entry);
            Logg.Info(_navigation.Main);
        }

        private void OnPushA(Unit unit)
        {
            Logg.Info("Push A input");
            _navigation.Main.Push(NavigationConsts.MAIN_SCENE_A, _bundle++);
        }

        private void OnPushB(Unit unit)
        {
            Logg.Info("Push B input");
            _navigation.Main.Push(NavigationConsts.MAIN_SCENE_B, _bundle++);
        }

        private void OnPop(Unit unit)
        {
            Logg.Info("Pop input");
            _navigation.Main.Pop();
        }

        private void OnReplaceA(Unit unit)
        {
            Logg.Info("Replace A input");
            _navigation.Main.Replace(NavigationConsts.MAIN_SCENE_A, _bundle++);
        }

        private void OnReplaceB(Unit unit)
        {
            Logg.Info("Replace B input");
            _navigation.Main.Replace(NavigationConsts.MAIN_SCENE_B, _bundle++);
        }

        private void OnResetContext(Unit unit)
        {
            Logg.Info("Try to reset context");
            _navigation.Context.Add(NavigationConsts.CONTEXT_SCENE, _bundle++);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
