using UniCore.Examples.Navigation;
using UnityEngine;
using Zenject;

namespace CTA.Examples.Navigation
{
    public class ContextInstaller : MonoInstaller
    {
        [SerializeField] private ContextController _controller;

        public override void InstallBindings()
        {
            Container.Bind<ContextController>().FromInstance(_controller);
            Container.BindInterfacesAndSelfTo<ContextManager>().AsSingle().NonLazy();
        }
    }
}
