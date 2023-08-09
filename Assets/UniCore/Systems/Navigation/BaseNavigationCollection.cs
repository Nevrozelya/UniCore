using System.Collections.Generic;
using UniCore.Extensions;
using UniCore.Utils;

namespace UniCore.Systems.Navigation
{
    public abstract class BaseNavigationCollection
    {
        protected Logg _log;
        protected HashSet<string> _existingSceneNames;

        public BaseNavigationCollection(params string[] existingScenes)
        {
            _log = new(this);
            _existingSceneNames = new(existingScenes);
        }

        public bool IsExisting(string sceneName)
        {
            if (!_existingSceneNames.IsNullOrEmpty())
            {
                return _existingSceneNames.Contains(sceneName);
            }

            return true;
        }
    }
}
