using System.Collections.Generic;
using UniCore.Extensions;
using UnityEngine.SceneManagement;
using Zenject;

namespace UniCore.Systems.Navigation
{
    public class NavigationSystem : IInitializable
    {
        public NavigationStack MainScenes { get; private set; }
        public NavigationGroup ContextScenes { get; private set; }
        public NavigationGroup TransitionScenes { get; private set; }

        public Scene MainScene => MainScenes.Current.Scene;
        private HashSet<string> _mainSceneNames;

        public NavigationSystem(params string[] mainSceneNames)
        {
            _mainSceneNames = new();

            if (mainSceneNames.IsNullOrEmpty())
            {
                foreach (string sceneName in mainSceneNames)
                {
                    _mainSceneNames.Add(sceneName);
                }
            }
        }

        public void Initialize()
        {
            MainScenes = new(null);
            ContextScenes = new();
            TransitionScenes = new();
        }
    }
}
