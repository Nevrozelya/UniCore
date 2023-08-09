using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation
{
    public class NavigationEntry
    {
        public string SceneName => Scene.name;
        public string ScenePath => Scene.path;

        public Scene Scene { get; private set; }
        public object Bundle { get; private set; }

        public NavigationEntry(Scene scene, object bundle = null)
        {
            Scene = scene;
            Bundle = bundle;
        }
    }
}
