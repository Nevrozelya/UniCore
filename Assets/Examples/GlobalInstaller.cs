using UniCore.Examples.Navigation;
using UnityEngine;
using Zenject;

namespace UniCore.Examples
{
    [CreateAssetMenu(fileName = "GlobalInstaller", menuName = "UniCore/Examples/GlobalInstaller")]
    public class GlobalInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<NavigationService>().AsSingle().NonLazy();
        }
    }
}
