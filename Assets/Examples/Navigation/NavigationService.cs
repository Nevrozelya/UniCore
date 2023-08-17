using System;
using UniCore.Systems.Navigation;
using UniCore.Systems.Navigation.Collections;
using Zenject;

namespace UniCore.Examples.Navigation
{
    public class NavigationService : IInitializable, IDisposable
    {
        public NavigationStack Main => _system.MainScenes;
        public NavigationGroup Context => _system.ContextScenes;
        public NavigationGroup Transition => _system.TransitionScenes;

        private NavigationSystem _system;

        public void Initialize()
        {
            string[] mainSceneNames = new[]
            {
                NavigationConsts.MAIN_SCENE_A,
                NavigationConsts.MAIN_SCENE_B,
            };

            string[] contextSceneNames = new[]
            {
                NavigationConsts.CONTEXT_SCENE
            };

            NavigationSetup setup = new
            (
                mainSceneNames,
                contextSceneNames,
                transitionSceneNames: null,
                mainConduct: NavigationCollectionConduct.Replace,
                contextConduct: NavigationCollectionConduct.Forbidden,
                transitionConduct: NavigationCollectionConduct.Forbidden,
                autoLoadContext: true
            );

            _system = new(setup);
        }

        public void Dispose()
        {
            _system?.Dispose();
        }
    }
}
