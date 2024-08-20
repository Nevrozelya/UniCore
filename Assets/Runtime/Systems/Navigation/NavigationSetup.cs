using UniCore.Systems.Navigation.Collections;

namespace UniCore.Systems.Navigation
{
    public class NavigationSetup
    {
        public string[] MainSceneNames { get; private set; }
        public string[] ContextSceneNames { get; private set; }
        public string[] TransitionSceneNames { get; private set; }

        public NavigationCollectionConduct MainConduct { get; private set; }
        public NavigationCollectionConduct ContextConduct { get; private set; }
        public NavigationCollectionConduct TransitionConduct { get; private set; }

        public bool AutoLoadContext { get; private set; }

        public NavigationSetup(
            string[] mainSceneNames,
            string[] contextSceneNames,
            string[] transitionSceneNames,
            NavigationCollectionConduct mainConduct,
            NavigationCollectionConduct contextConduct,
            NavigationCollectionConduct transitionConduct,
            bool autoLoadContext)
        {
            MainSceneNames = mainSceneNames;
            ContextSceneNames = contextSceneNames;
            TransitionSceneNames = transitionSceneNames;
            MainConduct = mainConduct;
            ContextConduct = contextConduct;
            TransitionConduct = transitionConduct;
            AutoLoadContext = autoLoadContext;
        }
    }
}
