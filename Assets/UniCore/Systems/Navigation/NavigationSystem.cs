namespace UniCore.Systems.Navigation
{
    public class NavigationSystem
    {
        public NavigationStack MainScenes { get; private set; }
        public NavigationGroup ContextScenes { get; private set; }
        public NavigationGroup TransitionScenes { get; private set; }

        public NavigationSystem(NavigationSetup setup)
        {
            if (setup == null)
            {
                MainScenes = new();
                ContextScenes = new();
                TransitionScenes = new();
            }
            else
            {
                MainScenes = new(setup.MainSceneNames);
                ContextScenes = new(setup.ContextSceneNames);
                TransitionScenes = new(setup.TransitionSceneNames);
            }
        }
    }
}
