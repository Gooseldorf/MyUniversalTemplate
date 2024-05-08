using Infrastructure.AssetManagement;
using Infrastructure.Factories;

namespace Infrastructure.DI
{
    public class BootstrapInstaller : MonoInstallerBase
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IBootstrapFactory>().To<BootstrapFactory>().AsSingle();
        }
    }
}