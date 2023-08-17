using System;
using UniCore.Systems.Navigation;
using Zenject;

namespace UniCore.Examples.Navigation
{
    public class NavigationService : IInitializable, IDisposable
    {
        public NavigationSystem System { get; private set; }

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
                autoLoadContext: true,
                allowMainSceneOverride: false
            );

            System = new(setup);
        }

        public void Dispose()
        {
            System?.Dispose();
        }
    }
}
