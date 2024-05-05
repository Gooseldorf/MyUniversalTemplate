using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Zenject;

namespace Infrastructure.DI
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }
    }
}