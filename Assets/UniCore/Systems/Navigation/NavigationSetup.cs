namespace UniCore.Systems.Navigation
{
    public class NavigationSetup
    {
        public string[] MainSceneNames { get; private set; }
        public string[] ContextSceneNames { get; private set; }
        public string[] TransitionSceneNames { get; private set; }

        public bool AutoLoadContext { get; private set; }
        public bool AllowMainSceneOverride { get; private set; }

        public NavigationSetup(string[] mainSceneNames, string[] contextSceneNames, string[] transitionSceneNames, bool autoLoadContext, bool allowMainSceneOverride)
        {
            MainSceneNames = mainSceneNames;
            ContextSceneNames = contextSceneNames;
            TransitionSceneNames = transitionSceneNames;
            AutoLoadContext = autoLoadContext;
            AllowMainSceneOverride = allowMainSceneOverride;
        }
    }
}
