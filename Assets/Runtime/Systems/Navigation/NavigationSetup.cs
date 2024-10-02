using UniCore.Systems.Navigation.Collections;

namespace UniCore.Systems.Navigation
{
    public class NavigationSetup
    {
        public readonly string[] MainSceneNames;
        public readonly string[] ContextSceneNames;
        public readonly string[] TransitionSceneNames;

        public readonly NavigationCollectionConduct MainConduct;
        public readonly NavigationCollectionConduct ContextConduct;
        public readonly NavigationCollectionConduct TransitionConduct;

        public readonly bool AutoLoadContext;

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
