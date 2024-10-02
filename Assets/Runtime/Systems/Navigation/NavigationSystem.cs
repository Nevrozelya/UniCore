using UniCore.Systems.Navigation.Collections;
using UniCore.Utils;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public class NavigationSystem
    {
        public NavigationStack MainScenes { get; private set; }
        public NavigationGroup ContextScenes { get; private set; }
        public NavigationGroup TransitionScenes { get; private set; }

        public NavigationSystem(NavigationSetup setup)
        {
            if (setup != null)
            {
                Scene[] currentlyLoaded = NavigationUtils.GetLoadedScenes();

                MainScenes = new(
                    setup.MainSceneNames,
                    currentlyLoaded,
                    setup.MainConduct);

                ContextScenes = new(
                    setup.ContextSceneNames,
                    currentlyLoaded,
                    setup.ContextConduct,
                    autoLoad: setup.AutoLoadContext);

                TransitionScenes = new(
                    setup.TransitionSceneNames,
                    currentlyLoaded,
                    setup.TransitionConduct,
                    autoLoad: false);
            }
            else
            {
                Logg.Error("Given setup is null!", "NavigationSystem");
            }
        }
    }
}
