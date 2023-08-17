using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation.Collections
{
    public class NavigationEntry
    {
        public Scene Scene { get; private set; }
        public object Bundle { get; private set; }
        public string SceneName { get; private set; }
        public string ScenePath { get; private set; }
        public bool IsLoaded => Scene.isLoaded;
        public bool HasBundle => Bundle != null;

        public NavigationEntry(Scene scene, object bundle = null)
        {
            Scene = scene;
            Bundle = bundle;

            // NOTE: Those 2 are saved here
            // and not directly returned via => Scene.name
            // because they are empty when the Scene
            // is not loaded!
            SceneName = Scene.name;
            ScenePath = Scene.path;
        }

        public override string ToString()
        {
            if (Bundle != null)
            {
                return $"{SceneName} ({Bundle})";
            }
            else
            {
                return SceneName;
            }
        }
    }
}
