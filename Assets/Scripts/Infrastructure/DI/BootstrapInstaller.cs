using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure.DI
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }
    }
}