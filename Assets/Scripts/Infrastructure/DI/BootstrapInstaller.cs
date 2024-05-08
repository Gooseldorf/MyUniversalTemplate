using Infrastructure.AssetManagement;
using Infrastructure.Services.Input;
using Zenject;

namespace Infrastructure.DI
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            //Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IInputService>().FromInstance(new InputService()).AsSingle();
        }
    }
}