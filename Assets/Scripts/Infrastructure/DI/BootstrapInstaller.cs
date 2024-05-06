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
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }
    }
}