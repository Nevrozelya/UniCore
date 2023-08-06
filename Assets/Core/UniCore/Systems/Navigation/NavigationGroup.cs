using System.Collections.Generic;

namespace UniCore.Systems.Navigation
{
    public class NavigationGroup
    {
        private Dictionary<string, NavigationEntry> _group;

        public NavigationGroup()
        {
            _group = new();
        }
    }
}
