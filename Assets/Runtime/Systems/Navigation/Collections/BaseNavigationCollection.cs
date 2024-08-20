using System.Collections.Generic;
using UniCore.Extensions;
using UniCore.Utils;
using UnityEngine.SceneManagement;

namespace UniCore.Systems.Navigation.Collections
{
    public abstract class BaseNavigationCollection
    {
        protected Logg _log;
        protected HashSet<string> _validScenes;
        protected NavigationCollectionConduct _conduct;

        public BaseNavigationCollection(string[] validScenes, Scene[] loadedScenes, NavigationCollectionConduct conduct)
        {
            _log = new(this);
            _conduct = conduct;

            if (!validScenes.IsNullOrEmpty())
            {
                _validScenes = new(validScenes);

                if (!loadedScenes.IsNullOrEmpty())
                {
                    foreach (Scene scene in loadedScenes)
                    {
                        if (_validScenes.Contains(scene.name))
                        {
                            SetInitiallyLoadedScene(scene);
                        }
                    }
                }
            }
        }

        public bool IsValid(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                return false;
            }

            if (!_validScenes.IsNullOrEmpty())
            {
                return _validScenes.Contains(sceneName);
            }

            return true;
        }

        protected abstract void SetInitiallyLoadedScene(Scene scene);
    }
}
